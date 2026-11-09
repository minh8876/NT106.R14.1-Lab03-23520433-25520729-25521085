using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Bai02
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;

            Thread serverThread = new Thread(new ThreadStart(StartUnsafeThread));
            serverThread.IsBackground = true;
            serverThread.Start();
        }

        void StartUnsafeThread()
        {
            int bytesReceived = 0;
            byte[] recv = new byte[1];

            Socket clientSocket;

            Socket listenerSocket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp
            );

            IPEndPoint ipepServer = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);

            listenerSocket.Bind(ipepServer);

            listenerSocket.Listen(-1);

            listViewCommand.Items.Add(new ListViewItem("Telnet running on 127.0.0.1:8080"));

            clientSocket = listenerSocket.Accept();

            listViewCommand.Items.Add(new ListViewItem("New client connected"));

            while (clientSocket.Connected)
            {
                string text = "";
                do
                {
                    bytesReceived = clientSocket.Receive(recv);

                    if (bytesReceived == 0) break;

                    text += Encoding.ASCII.GetString(recv);
                } while (text[text.Length - 1] != '\n');

                if (bytesReceived == 0) break;

                listViewCommand.Items.Add(new ListViewItem(text.Trim()));
            }

            listenerSocket.Close();
        }
    }
}
