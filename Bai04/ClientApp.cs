using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Bai04
{
    public partial class ClientApp : Form
    {
        private TcpClient client;
        private StreamReader reader;
        private StreamWriter writer;

        private Dictionary<string, List<int>> movieRooms = new Dictionary<string, List<int>>();
        private HashSet<string> soldSeats = new HashSet<string>();

        private readonly string[] allSeats = {
            "A1", "A2", "A3", "A4", "A5",
            "B1", "B2", "B3", "B4", "B5",
            "C1", "C2", "C3", "C4", "C5"
        };

        public bool IsConnected { get; private set; } = false;

        public ClientApp()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            ConnectToServer();
        }

        private void ConnectToServer()
        {
            try
            {
                client = new TcpClient("127.0.0.1", 8888);
                reader = new StreamReader(client.GetStream(), Encoding.UTF8);
                writer = new StreamWriter(client.GetStream(), Encoding.UTF8) { AutoFlush = true };

                Thread listenThread = new Thread(ReceiveData) { IsBackground = true };
                listenThread.Start();

                IsConnected = true;
            }
            catch
            {
                MessageBox.Show("Không thể kết nối đến Server! Hãy bật Server và bấm Listen trước.");
                IsConnected = false;
            }
        }

        private void ReceiveData()
        {
            try
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split('|');
                    string cmd = parts[0];

                    if (cmd == "INIT")
                    {
                        string[] movies = parts[1].Split('#');
                        cboMovie.Items.Clear();
                        foreach (string m in movies)
                        {
                            string[] info = m.Split(';');
                            string name = info[0];
                            List<int> rooms = info[2].Split(',').Select(int.Parse).ToList();
                            movieRooms[name] = rooms;
                            cboMovie.Items.Add(name);
                        }
                        if (cboMovie.Items.Count > 0) cboMovie.SelectedIndex = 0;
                    }
                    else if (cmd == "UPDATE")
                    {
                        string[] seats = parts[1].Split(',');
                        foreach (string s in seats)
                        {
                            if (!string.IsNullOrWhiteSpace(s))
                                soldSeats.Add(s.Trim());
                        }
                        RefreshSeatDisplay();
                    }
                    else if (cmd == "RESULT")
                    {
                        if (parts[1] == "OK")
                            MessageBox.Show("Đặt vé thành công!\n\n" + parts[2], "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show(parts[2], "Lỗi đặt vé", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi bên trong ReceiveData: " + ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomer.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên khách hàng!");
                return;
            }

            List<string> selectedSeats = new List<string>();
            foreach (var item in clbSeats.CheckedItems)
            {
                string s = item.ToString();
                if (!s.Contains("(Đã đặt)")) selectedSeats.Add(s);
            }

            if (selectedSeats.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất 1 ghế trống!");
                return;
            }

            string customer = txtCustomer.Text.Trim();
            string movie = cboMovie.SelectedItem.ToString();
            string room = cboRoom.SelectedItem.ToString();

            writer.WriteLine($"BOOK|{customer}|{movie}|{room}|{string.Join(",", selectedSeats)}");
        }

        private void cboMovie_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedMovie = cboMovie.SelectedItem.ToString();
            cboRoom.Items.Clear();
            foreach (int r in movieRooms[selectedMovie])
            {
                cboRoom.Items.Add(r);
            }
            if (cboRoom.Items.Count > 0) cboRoom.SelectedIndex = 0;
        }

        private void cboRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshSeatDisplay();
        }

        private void RefreshSeatDisplay()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(RefreshSeatDisplay));
                return;
            }

            if (cboRoom.SelectedItem == null) return;
            string currentRoom = cboRoom.SelectedItem.ToString().Trim();

            clbSeats.ItemCheck -= clbSeats_ItemCheck;

            clbSeats.Items.Clear();
            foreach (string seat in allSeats)
            {
                string key = $"{currentRoom}-{seat}";
                if (soldSeats.Contains(key))
                    clbSeats.Items.Add($"{seat} (Đã đặt)", CheckState.Indeterminate);
                else
                    clbSeats.Items.Add(seat, CheckState.Unchecked);
            }

            clbSeats.ItemCheck += clbSeats_ItemCheck;
        }

        private void clbSeats_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.Index >= clbSeats.Items.Count) return;

            string itemText = clbSeats.Items[e.Index].ToString();

            if (itemText.Contains("(Đã đặt)"))
            {
                e.NewValue = CheckState.Indeterminate;
                return;
            }

            if (e.NewValue == CheckState.Checked)
            {
                int currentCheckedCount = 0;
                foreach (var item in clbSeats.CheckedItems)
                {
                    if (!item.ToString().Contains("(Đã đặt)")) currentCheckedCount++;
                }

                if (currentCheckedCount >= 2)
                {
                    MessageBox.Show("Bạn không được chọn nhiều hơn 2 vé!", "Giới hạn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.NewValue = CheckState.Unchecked;
                }
            }
        }
    }
}
