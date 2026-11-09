using System;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Bai03_TCP
{
    public class FormClient : Form
    {
        private readonly TextBox txtIP;
        private readonly TextBox txtPort;
        private readonly TextBox txtMessage;
        private readonly Button btnConnect;
        private readonly Button btnSend;
        private readonly Button btnDisconnect;
        private readonly Label lblStatus;
        private readonly ListBox lstMessages;

        private TcpClient tcpClient;
        private NetworkStream networkStream;
        private StreamWriter writer;

        public FormClient()
        {
            Text = "TCP Client";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(700, 500);
            MinimumSize = new Size(600, 420);

            var title = new Label
            {
                Text = "CLIENT - GỬI THÔNG ĐIỆP ĐẾN SERVER",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 18)
            };

            var lblIP = new Label { Text = "IP Server:", AutoSize = true, Location = new Point(22, 70) };
            txtIP = new TextBox { Text = "127.0.0.1", Location = new Point(95, 66), Width = 150 };

            var lblPort = new Label { Text = "Port:", AutoSize = true, Location = new Point(270, 70) };
            txtPort = new TextBox { Text = "8080", Location = new Point(315, 66), Width = 80 };

            btnConnect = new Button { Text = "Connect", Location = new Point(420, 64), Size = new Size(100, 32) };
            btnConnect.Click += BtnConnect_Click;

            lblStatus = new Label
            {
                Text = "Trạng thái: Chưa kết nối",
                AutoSize = true,
                Location = new Point(22, 112),
                ForeColor = Color.DarkRed
            };

            var lblMessage = new Label { Text = "Thông điệp:", AutoSize = true, Location = new Point(22, 155) };
            txtMessage = new TextBox
            {
                Location = new Point(105, 151),
                Width = 390,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            txtMessage.KeyDown += TxtMessage_KeyDown;

            btnSend = new Button
            {
                Text = "Send",
                Location = new Point(510, 149),
                Size = new Size(75, 30),
                Enabled = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            btnSend.Click += BtnSend_Click;

            btnDisconnect = new Button
            {
                Text = "Disconnect",
                Location = new Point(595, 149),
                Size = new Size(80, 30),
                Enabled = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            btnDisconnect.Click += BtnDisconnect_Click;

            lstMessages = new ListBox
            {
                Location = new Point(22, 205),
                Size = new Size(650, 230),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                HorizontalScrollbar = true
            };

            Controls.AddRange(new Control[]
            {
                title, lblIP, txtIP, lblPort, txtPort, btnConnect,
                lblStatus, lblMessage, txtMessage, btnSend, btnDisconnect, lstMessages
            });

            FormClosing += FormClient_FormClosing;
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            ConnectServer();
        }

        private void ConnectServer()
        {
            try
            {
                if (!int.TryParse(txtPort.Text.Trim(), out int port) ||
                    port < 1 || port > 65535)
                {
                    MessageBox.Show("Port phải là số nguyên từ 1 đến 65535.");
                    return;
                }

                tcpClient = new TcpClient();
                tcpClient.Connect(txtIP.Text.Trim(), port);

                networkStream = tcpClient.GetStream();
                writer = new StreamWriter(networkStream, new UTF8Encoding(false))
                {
                    AutoFlush = true
                };

                lblStatus.Text = "Trạng thái: Đã kết nối";
                lblStatus.ForeColor = Color.DarkGreen;
                lstMessages.Items.Add("Đã kết nối đến " + txtIP.Text.Trim() + ":" + port);

                btnConnect.Enabled = false;
                btnSend.Enabled = true;
                btnDisconnect.Enabled = true;
                txtIP.Enabled = false;
                txtPort.Enabled = false;
                txtMessage.Focus();
            }
            catch (Exception ex)
            {
                lstMessages.Items.Add("Lỗi kết nối: " + ex.Message);
                CloseConnection();
            }
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void TxtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && btnSend.Enabled)
            {
                e.SuppressKeyPress = true;
                SendMessage();
            }
        }

        private void SendMessage()
        {
            if (writer == null || tcpClient == null || !tcpClient.Connected)
            {
                MessageBox.Show("Bạn chưa kết nối Server.");
                return;
            }

            string message = txtMessage.Text.Trim();
            if (string.IsNullOrWhiteSpace(message)) return;

            try
            {
                writer.WriteLine(message);
                lstMessages.Items.Add("Tôi: " + message);
                txtMessage.Clear();
                txtMessage.Focus();
            }
            catch (Exception ex)
            {
                lstMessages.Items.Add("Lỗi gửi dữ liệu: " + ex.Message);
                CloseConnection();
            }
        }

        private void BtnDisconnect_Click(object sender, EventArgs e)
        {
            DisconnectServer();
        }

        private void DisconnectServer()
        {
            try
            {
                if (writer != null)
                    writer.WriteLine("quit");
            }
            catch { }

            CloseConnection();
            lstMessages.Items.Add("Đã ngắt kết nối.");
        }

        private void CloseConnection()
        {
            try { writer?.Dispose(); } catch { }
            try { networkStream?.Close(); } catch { }
            try { tcpClient?.Close(); } catch { }

            writer = null;
            networkStream = null;
            tcpClient = null;

            if (!IsDisposed)
            {
                lblStatus.Text = "Trạng thái: Chưa kết nối";
                lblStatus.ForeColor = Color.DarkRed;
                btnConnect.Enabled = true;
                btnSend.Enabled = false;
                btnDisconnect.Enabled = false;
                txtIP.Enabled = true;
                txtPort.Enabled = true;
            }
        }

        private void FormClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisconnectServer();
        }
    }
}
