using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Bai03_TCP
{
    public class FormServer : Form
    {
        private const int ServerPort = 9000;

        private readonly Button btnListen;
        private readonly Button btnStop;
        private readonly Label lblStatus;
        private readonly ListBox lstMessages;

        private TcpListener listener;
        private Thread serverThread;
        private volatile bool isRunning;

        public FormServer()
        {
            Text = "TCP Server";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(650, 430);
            MinimumSize = new Size(550, 350);

            var title = new Label
            {
                Text = "SERVER - LẮNG NGHE KẾT NỐI TCP",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            lblStatus = new Label
            {
                Text = "Trạng thái: Chưa chạy",
                AutoSize = true,
                Location = new Point(22, 65),
                ForeColor = Color.DarkRed
            };

            btnListen = new Button
            {
                Text = "Listen",
                Location = new Point(20, 95),
                Size = new Size(110, 35)
            };
            btnListen.Click += BtnListen_Click;

            btnStop = new Button
            {
                Text = "Stop",
                Location = new Point(145, 95),
                Size = new Size(110, 35),
                Enabled = false
            };
            btnStop.Click += BtnStop_Click;

            lstMessages = new ListBox
            {
                Location = new Point(20, 150),
                Size = new Size(590, 220),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                HorizontalScrollbar = true
            };

            Controls.AddRange(new Control[] { title, lblStatus, btnListen, btnStop, lstMessages });
            FormClosing += FormServer_FormClosing;
        }

        private void BtnListen_Click(object sender, EventArgs e)
        {
            if (isRunning) return;

            isRunning = true;
            btnListen.Enabled = false;
            btnStop.Enabled = true;
            lblStatus.Text = "Trạng thái: Đang khởi động...";
            lblStatus.ForeColor = Color.DarkOrange;

            serverThread = new Thread(StartServer)
            {
                IsBackground = true,
                Name = "TCP-Server-Thread"
            };
            serverThread.Start();
        }

        private void StartServer()
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, ServerPort);
                listener.Start();
                AddMessage("Server đang lắng nghe tại cổng " + ServerPort + ".");
                SetStatus("Trạng thái: Đang lắng nghe", Color.DarkGreen);

                while (isRunning)
                {
                    TcpClient connectedClient = null;

                    try
                    {
                        // Chờ một Client kết nối. Stop() sẽ giải phóng lệnh chờ này.
                        connectedClient = listener.AcceptTcpClient();
                        AddMessage("Client đã kết nối: " +
                            connectedClient.Client.RemoteEndPoint);

                        using (connectedClient)
                        using (NetworkStream stream = connectedClient.GetStream())
                        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            string message;
                            while (isRunning && (message = reader.ReadLine()) != null)
                            {
                                if (message == "quit")
                                    break;

                                AddMessage("Client: " + message);
                            }
                        }

                        AddMessage("Client đã ngắt kết nối.");
                    }
                    catch (SocketException)
                    {
                        if (isRunning)
                            throw;
                    }
                    finally
                    {
                        if (connectedClient != null)
                            connectedClient.Close();
                    }
                }
            }
            catch (SocketException ex)
            {
                if (isRunning)
                    AddMessage("Lỗi Socket: " + ex.Message);
            }
            catch (Exception ex)
            {
                if (isRunning)
                    AddMessage("Lỗi Server: " + ex.Message);
            }
            finally
            {
                isRunning = false;
                try { listener?.Stop(); } catch { }
                SetStatus("Trạng thái: Đã dừng", Color.DarkRed);
                SetServerButtons(listenEnabled: true, stopEnabled: false);
                AddMessage("Server đã dừng.");
            }
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            StopServer();
        }

        private void StopServer()
        {
            isRunning = false;
            try { listener?.Stop(); } catch { }
            SetStatus("Trạng thái: Đang dừng...", Color.DarkOrange);
            btnStop.Enabled = false;
        }

        private void AddMessage(string message)
        {
            if (IsDisposed || !IsHandleCreated) return;

            if (InvokeRequired)
            {
                try { BeginInvoke(new Action<string>(AddMessage), message); }
                catch (InvalidOperationException) { }
            }
            else
            {
                lstMessages.Items.Add("[" + DateTime.Now.ToString("HH:mm:ss") + "] " + message);
                lstMessages.TopIndex = lstMessages.Items.Count - 1;
            }
        }

        private void SetStatus(string text, Color color)
        {
            if (IsDisposed || !IsHandleCreated) return;

            if (InvokeRequired)
            {
                try { BeginInvoke(new Action<string, Color>(SetStatus), text, color); }
                catch (InvalidOperationException) { }
            }
            else
            {
                lblStatus.Text = text;
                lblStatus.ForeColor = color;
            }
        }

        private void SetServerButtons(bool listenEnabled, bool stopEnabled)
        {
            if (IsDisposed || !IsHandleCreated) return;

            if (InvokeRequired)
            {
                try { BeginInvoke(new Action<bool, bool>(SetServerButtons), listenEnabled, stopEnabled); }
                catch (InvalidOperationException) { }
            }
            else
            {
                btnListen.Enabled = listenEnabled;
                btnStop.Enabled = stopEnabled;
            }
        }

        private void FormServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopServer();
        }
    }
}
