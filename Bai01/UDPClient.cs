using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Bai01
{
    public partial class UDPClient : Form
    {
        public UDPClient()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                UdpClient udpClient = new UdpClient();

                IPAddress ipadd = IPAddress.Parse(txtIP.Text.Trim());
                int port = Convert.ToInt32(txtPort.Text.Trim());
                IPEndPoint ipend = new IPEndPoint(ipadd, port);

                byte[] sendBytes = Encoding.UTF8.GetBytes(rtbMessage.Text);

                udpClient.Send(sendBytes, sendBytes.Length, ipend);

                rtbMessage.Clear();
                udpClient.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi gửi dữ liệu: " + ex.Message);
            }
        }
    }
}
