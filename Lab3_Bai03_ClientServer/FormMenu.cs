using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bai03_TCP
{
    public class FormMenu : Form
    {
        public FormMenu()
        {
            Text = "Bài 3 - TCP Client / Server";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(520, 300);
            MinimumSize = new Size(520, 300);

            var title = new Label
            {
                Text = "BÀI 3: 1 SERVER - 1 CLIENT",
                Font = new Font("Segoe UI", 17, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(75, 35)
            };

            var description = new Label
            {
                Text = "Ứng dụng gửi và nhận thông điệp bằng giao thức TCP",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(75, 85)
            };

            var btnServer = new Button
            {
                Text = "Mở Server",
                Size = new Size(150, 48),
                Location = new Point(75, 145)
            };
            btnServer.Click += (s, e) =>
            {
                var form = new FormServer();
                form.Show(this);
            };

            var btnClient = new Button
            {
                Text = "Mở Client",
                Size = new Size(150, 48),
                Location = new Point(265, 145)
            };
            btnClient.Click += (s, e) =>
            {
                var form = new FormClient();
                form.Show(this);
            };

            Controls.AddRange(new Control[] { title, description, btnServer, btnClient });
        }
    }
}
