using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection.Metadata.Ecma335;

namespace Bai01
{
    public partial class UDPServer : Form
    {
        private UdpClient? udpServer;
        private Thread? thdUDPServer;

        public UDPServer()
        {
            InitializeComponent();
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            try
            {
                thdUDPServer = new Thread(new ThreadStart(serverThread));
                thdUDPServer.IsBackground = true;
                thdUDPServer.Start();

                btnListen.Enabled = false;
                txtPort.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo server: " + ex.Message);
            }
        }

        public void serverThread()
        {
            int port = Convert.ToInt32(txtPort.Text.Trim());
            udpServer = new UdpClient(port);

            while (true)
            {
                try
                {
                    IPEndPoint RemoteIPEndPoint = new IPEndPoint(IPAddress.Any, 0);

                    Byte[] receiveBytes = udpServer.Receive(ref RemoteIPEndPoint);

                    string returnData = Encoding.UTF8.GetString(receiveBytes);

                    string mess = RemoteIPEndPoint.Address.ToString() + ":" + returnData;

                    InfoMessage(mess);
                }
                catch
                {
                    break;
                }
            }
        }

        public void InfoMessage(string message)
        {
            if (lvMessages.InvokeRequired)
            {
                lvMessages.Invoke(new Action<string>(InfoMessage), new object[] { message });
            }
            else
            {
                lvMessages.Items.Add(new ListViewItem(message));
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            udpServer?.Close();
        }
    }
}
