using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Bai04
{
    public partial class Dashboard : Form
    {
        private ServerApp serverForm = null;
        private int clientCount = 0;

        public Dashboard()
        {
            InitializeComponent();
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            clientCount++;
            ClientApp clientForm = new ClientApp();
            clientForm.Text = $"Quầy bán vé số {clientCount}";
            clientForm.Show();
        }

        private void btnServer_Click(object sender, EventArgs e)
        {
            if (serverForm == null || serverForm.IsDisposed)
            {
                serverForm = new ServerApp();
                serverForm.Show();
                btnServer.Enabled = false;
            }
        }
    }
}
