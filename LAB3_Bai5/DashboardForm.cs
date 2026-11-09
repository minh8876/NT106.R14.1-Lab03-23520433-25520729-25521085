using System;
using System.Windows.Forms;

namespace LAB3_Bai5
{
    public partial class DashboardForm : Form
    {
        private ServerForm? serverForm;

        public DashboardForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (serverForm == null || serverForm.IsDisposed)
            {
                serverForm = new ServerForm();
                serverForm.Show();
            }
            else
            {
                serverForm.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClientForm client = new ClientForm();
            client.Show();
        }
    }
}