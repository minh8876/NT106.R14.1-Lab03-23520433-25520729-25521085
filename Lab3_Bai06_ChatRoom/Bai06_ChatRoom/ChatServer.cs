using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
namespace Bai06_ChatRoom;
public sealed class ChatServer {
 readonly TcpListener listener; readonly Action<string> log; readonly CancellationTokenSource cancel=new();
 readonly ConcurrentDictionary<string, Peer> peers=new();
 public ChatServer(int port,Action<string> logger){listener=new TcpListener(IPAddress.Any,port);log=logger;}
 public Task StartAsync(){listener.Start(); _=AcceptLoop(); return Task.CompletedTask;}
 async Task AcceptLoop(){
  try { while(!cancel.IsCancellationRequested){var tcp=await listener.AcceptTcpClientAsync(cancel.Token); _=Handle(tcp);} }
  catch(OperationCanceledException){} catch(ObjectDisposedException){} catch(Exception ex){log("Accept: "+ex.Message);}
 }
 async Task Handle(TcpClient tcp){
  string name=""; Peer? peer=null;
  try {
   using(tcp) using(var ns=tcp.GetStream()) using(var rd=new StreamReader(ns,Encoding.UTF8)) using(var wr=new StreamWriter(ns,new UTF8Encoding(false)){AutoFlush=true}) {
    var first=await rd.ReadLineAsync(cancel.Token); if(first==null)return;
    var hello=JsonSerializer.Deserialize<ChatMessage>(first); name=(hello?.Sender??"").Trim();
    if(name.Length==0||name.Length>20||peers.ContainsKey(name)){await wr.WriteLineAsync(JsonSerializer.Serialize(new ChatMessage{Type="system",Content="Tên không hợp lệ hoặc đang được sử dụng."}));return;}
    peer=new Peer(tcp,wr); if(!peers.TryAdd(name,peer))return;
    log(name+" đã kết nối."); await Broadcast(new ChatMessage{Type="system",Content=name+" đã tham gia."}); await SendUsers();
    while(!cancel.IsCancellationRequested){
     var line=await rd.ReadLineAsync(cancel.Token); if(line==null)break;
     var m=JsonSerializer.Deserialize<ChatMessage>(line); if(m==null)continue; m.Sender=name;
     if(m.Type=="public"){await Broadcast(m);log(name+" gửi tin nhắn nhóm.");}
     else if(m.Type=="private"&&!string.IsNullOrWhiteSpace(m.Recipient)){
      if(peers.TryGetValue(m.Recipient,out var target)){await target.Send(m);await peer.Send(m);log(name+" nhắn riêng "+m.Recipient);}
      else await peer.Send(new ChatMessage{Type="system",Content="Người nhận hiện không trực tuyến."});
     }
    }
   }
  } catch(OperationCanceledException){} catch(IOException){} catch(SocketException){} catch(Exception ex){log("Client "+name+": "+ex.Message);}
  finally { if(name!=""&&peer!=null){peers.TryRemove(name,out _);log(name+" đã ngắt kết nối.");await Broadcast(new ChatMessage{Type="system",Content=name+" đã rời phòng."});await SendUsers();} }
 }
 async Task Broadcast(ChatMessage m){string json=JsonSerializer.Serialize(m);foreach(var peer in peers.Values)await peer.SendRaw(json);}
 Task SendUsers()=>Broadcast(new ChatMessage{Type="users",Content=string.Join(",",peers.Keys.OrderBy(x=>x))});
 public void Stop(){cancel.Cancel();listener.Stop();foreach(var x in peers.Values)x.Close();peers.Clear();log("Server đã dừng.");}
 sealed class Peer {
  readonly TcpClient tcp; readonly StreamWriter writer; readonly SemaphoreSlim gate=new(1,1);
  public Peer(TcpClient c,StreamWriter w){tcp=c;writer=w;}
  public Task Send(ChatMessage m)=>SendRaw(JsonSerializer.Serialize(m));
  public async Task SendRaw(string s){await gate.WaitAsync();try{await writer.WriteLineAsync(s);}catch{}finally{gate.Release();}}
  public void Close(){try{tcp.Close();}catch{}}
 }
}