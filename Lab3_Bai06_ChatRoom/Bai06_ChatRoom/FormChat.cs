using System.Net.Sockets;
using System.Text;
using System.Text.Json;
namespace Bai06_ChatRoom;
public class FormChat : Form {
 readonly TextBox nameBox=new(),ipBox=new(),messages=new(),input=new();
 readonly ComboBox recipient=new(); readonly Button connect=new(),disconnect=new(),send=new();
 TcpClient? tcp; StreamReader? reader; StreamWriter? writer; CancellationTokenSource? receiveCancel;
 readonly SemaphoreSlim writeGate=new(1,1); string username="";
 public FormChat(){
  Text="Bài 6 - Chat Client";Width=800;Height=560;StartPosition=FormStartPosition.CenterScreen;
  var n=new Label{Text="Tên:",AutoSize=true,Left=15,Top=20};nameBox.SetBounds(55,15,130,28);nameBox.Text="User"+Random.Shared.Next(100,999);
  var ip=new Label{Text="Server IP:",AutoSize=true,Left=200,Top=20};ipBox.SetBounds(270,15,130,28);ipBox.Text="127.0.0.1";
  connect.Text="Connect";connect.SetBounds(415,14,90,30);disconnect.Text="Disconnect";disconnect.SetBounds(510,14,100,30);disconnect.Enabled=false;
  messages.Multiline=true;messages.ReadOnly=true;messages.ScrollBars=ScrollBars.Vertical;messages.SetBounds(15,60,710,360);messages.Anchor=AnchorStyles.Top|AnchorStyles.Bottom|AnchorStyles.Left|AnchorStyles.Right;
  var to=new Label{Text="Gửi đến:",AutoSize=true,Left=15,Top=445};recipient.SetBounds(80,440,180,28);recipient.DropDownStyle=ComboBoxStyle.DropDownList;recipient.Items.Add("Tất cả (Public)");recipient.SelectedIndex=0;
  input.SetBounds(345,440,270,28);input.Anchor=AnchorStyles.Bottom|AnchorStyles.Left|AnchorStyles.Right;
  var msg=new Label{Text="Tin nhắn:",AutoSize=true,Left=275,Top=445};send.Text="Send";send.SetBounds(630,438,95,32);send.Anchor=AnchorStyles.Bottom|AnchorStyles.Right;send.Enabled=false;
  Controls.AddRange(new Control[]{n,nameBox,ip,ipBox,connect,disconnect,messages,to,recipient,msg,input,send});
  connect.Click+=async(_,_)=>await Connect();disconnect.Click+=(_,_)=>Disconnect();send.Click+=async(_,_)=>await Send();
  input.KeyDown+=async(_,e)=>{if(e.KeyCode==Keys.Enter){e.SuppressKeyPress=true;await Send();}};FormClosing+=(_,_)=>Disconnect();
 }
 async Task Connect(){
  username=nameBox.Text.Trim();if(username.Length==0||username.Length>20){MessageBox.Show("Tên cần từ 1 đến 20 ký tự.");return;}
  try{
   tcp=new TcpClient();await tcp.ConnectAsync(ipBox.Text.Trim(),9000);var ns=tcp.GetStream();
   reader=new StreamReader(ns,Encoding.UTF8);writer=new StreamWriter(ns,new UTF8Encoding(false)){AutoFlush=true};
   await Write(new ChatMessage{Type="join",Sender=username});receiveCancel=new CancellationTokenSource();_=Receive(receiveCancel.Token);
   connect.Enabled=false;disconnect.Enabled=true;send.Enabled=true;nameBox.Enabled=false;ipBox.Enabled=false;Add("Đã kết nối Server.");
  }catch(Exception ex){Add("Kết nối thất bại: "+ex.Message);Disconnect();}
 }
 async Task Receive(CancellationToken token){
  try{while(!token.IsCancellationRequested&&reader!=null){var line=await reader.ReadLineAsync(token);if(line==null)break;var m=JsonSerializer.Deserialize<ChatMessage>(line);if(m==null)continue;
   if(m.Type=="users")UpdateUsers(m.Content);
   else if(m.Type=="system")Add("[Hệ thống] "+m.Content);
   else if(m.Type=="public")Add("[Nhóm] "+m.Sender+": "+m.Content);
   else if(m.Type=="private")Add("[Riêng] "+m.Sender+" → "+m.Recipient+": "+m.Content);
  }}catch(OperationCanceledException){}catch(IOException){}catch(Exception ex){Add("Lỗi nhận: "+ex.Message);}
 }
 async Task Send(){
  var text=input.Text.Trim();if(text.Length==0||writer==null)return;
  bool dm=recipient.SelectedIndex>0;var target=dm?recipient.SelectedItem?.ToString()??"": "";
  try{await Write(new ChatMessage{Type=dm?"private":"public",Sender=username,Recipient=target,Content=text});input.Clear();}
  catch(Exception ex){Add("Gửi thất bại: "+ex.Message);}
 }
 async Task Write(ChatMessage m){if(writer==null)return;await writeGate.WaitAsync();try{await writer.WriteLineAsync(JsonSerializer.Serialize(m));}finally{writeGate.Release();}}
 void UpdateUsers(string csv){
  if(IsDisposed||!IsHandleCreated)return;BeginInvoke(new Action(()=>{var old=recipient.SelectedItem?.ToString()??"Tất cả (Public)";recipient.Items.Clear();recipient.Items.Add("Tất cả (Public)");
   foreach(var u in csv.Split(',',StringSplitOptions.RemoveEmptyEntries))if(!u.Equals(username,StringComparison.OrdinalIgnoreCase))recipient.Items.Add(u);
   var i=recipient.Items.IndexOf(old);recipient.SelectedIndex=i>=0?i:0;}));
 }
 void Add(string s){if(IsDisposed||!IsHandleCreated)return;if(InvokeRequired){BeginInvoke(new Action<string>(Add),s);return;}messages.AppendText($"[{DateTime.Now:HH:mm:ss}] {s}{Environment.NewLine}");}
 void Disconnect(){receiveCancel?.Cancel();try{tcp?.Close();}catch{}try{reader?.Dispose();}catch{}try{writer?.Dispose();}catch{}tcp=null;reader=null;writer=null;
  if(!IsDisposed){connect.Enabled=true;disconnect.Enabled=false;send.Enabled=false;nameBox.Enabled=true;ipBox.Enabled=true;}}
}