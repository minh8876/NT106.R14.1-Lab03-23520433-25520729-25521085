using System.Net.Sockets;
namespace Bai06_ChatRoom;
public class FormServer : Form {
 readonly TextBox logBox=new(); readonly Button start=new(); readonly Button stop=new(); ChatServer? server;
 public FormServer() {
  Text="Bài 6 - Server"; Width=650; Height=420; StartPosition=FormStartPosition.CenterScreen;
  start.Text="Start Server"; start.SetBounds(15,15,125,35); stop.Text="Stop Server"; stop.SetBounds(150,15,125,35); stop.Enabled=false;
  logBox.Multiline=true; logBox.ReadOnly=true; logBox.ScrollBars=ScrollBars.Vertical; logBox.SetBounds(15,60,600,300); logBox.Anchor=AnchorStyles.Top|AnchorStyles.Bottom|AnchorStyles.Left|AnchorStyles.Right;
  Controls.AddRange(new Control[]{start,stop,logBox});
  start.Click+=async (_,_)=>{ try { server=new ChatServer(9000,AddLog); await server.StartAsync(); start.Enabled=false; stop.Enabled=true; AddLog("Server đang lắng nghe tại port 9000."); } catch(SocketException ex) { AddLog("Không mở được port: "+ex.Message); } };
  stop.Click+=(_,_)=>StopServer(); FormClosing+=(_,_)=>StopServer();
 }
 void StopServer(){ server?.Stop(); server=null; if(!IsDisposed){start.Enabled=true;stop.Enabled=false;} }
 void AddLog(string s){ if(IsDisposed||!IsHandleCreated)return; if(InvokeRequired){BeginInvoke(new Action<string>(AddLog),s);return;} logBox.AppendText($"[{DateTime.Now:HH:mm:ss}] {s}{Environment.NewLine}"); }
}