namespace Bai01
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            UDPClient clientForm = new UDPClient();
            clientForm.Show();
        }

        private void btnServer_Click(object sender, EventArgs e)
        {
            UDPServer serverForm = new UDPServer();
            serverForm.Show();
        }
    }
}
