namespace Bai06_ChatRoom;
public class FormMenu : Form {
 public FormMenu() {
  Text="Lab 3 - Bài 6: Chat Room"; Width=420; Height=240; StartPosition=FormStartPosition.CenterScreen;
  var title=new Label { Text="BÀI 6 - CHAT ROOM TCP", Dock=DockStyle.Top, Height=55, TextAlign=ContentAlignment.MiddleCenter, Font=new Font("Segoe UI",15,FontStyle.Bold) };
  var panel=new FlowLayoutPanel { Dock=DockStyle.Fill, FlowDirection=FlowDirection.TopDown, WrapContents=false, Padding=new Padding(95,20,0,0) };
  var server=new Button { Text="Mở Server", Width=200, Height=38 };
  var client=new Button { Text="Mở Chat Client", Width=200, Height=38 };
  server.Click += (_,_)=>new FormServer().Show(); client.Click += (_,_)=>new FormChat().Show();
  panel.Controls.Add(server); panel.Controls.Add(client); Controls.Add(panel); Controls.Add(title);
 }
}