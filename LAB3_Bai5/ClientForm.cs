using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LAB3_Bai5
{
    public partial class ClientForm : Form
    {
        private TcpClient? client;
        private StreamWriter? writer;
        private List<FoodItem> allFoods = new List<FoodItem>();
        private string selectedImageBase64 = "";

        public ClientForm()
        {
            InitializeComponent();

            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            if (listView1.Columns.Count == 0)
            {
                listView1.Columns.Add("ID", 50);
                listView1.Columns.Add("Tên món ăn", 150);
                listView1.Columns.Add("Người đóng góp", 150);
            }

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        }

        // button1: Kết nối Server
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                client = new TcpClient("127.0.0.1", 8080);
                writer = new StreamWriter(client.GetStream(), Encoding.UTF8) { AutoFlush = true };

                Thread receiveThread = new Thread(ReceiveData) { IsBackground = true };
                receiveThread.Start();
                button1.Enabled = false;
                MessageBox.Show("Kết nối Server thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối Server: " + ex.Message);
            }
        }

        private void ReceiveData()
        {
            if (client == null) return;
            StreamReader reader = new StreamReader(client.GetStream(), Encoding.UTF8);
            try
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("LIST|"))
                    {
                        ParseAndDisplayList(line.Substring(5));
                    }
                }
            }
            catch { }
        }

        private void ParseAndDisplayList(string data)
        {
            if (InvokeRequired) { Invoke(new Action(() => ParseAndDisplayList(data))); return; }

            allFoods.Clear();
            listView1.Items.Clear();

            if (string.IsNullOrEmpty(data)) return;

            foreach (string r in data.Split('#'))
            {
                string[] cols = r.Split(';');
                if (cols.Length >= 4)
                {
                    var item = new FoodItem { ID = cols[0], TenMon = cols[1], NguoiDongGop = cols[2], Base64Image = cols[3] };
                    allFoods.Add(item);

                    ListViewItem lvi = new ListViewItem(item.ID);
                    lvi.SubItems.Add(item.TenMon);
                    lvi.SubItems.Add(item.NguoiDongGop);
                    listView1.Items.Add(lvi);
                }
            }
        }

        // button2: Chọn ảnh...
        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog { Filter = "Image Files|*.jpg;*.jpeg;*.png" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    using (Image original = Image.FromFile(ofd.FileName))
                    using (Bitmap resized = new Bitmap(original, new Size(250, 250)))
                    using (MemoryStream ms = new MemoryStream())
                    {
                        resized.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        selectedImageBase64 = Convert.ToBase64String(ms.ToArray());
                        pictureBox1.Image = new Bitmap(resized);
                    }
                }
            }
        }

        // button3: Thêm món ăn
        private void button3_Click(object sender, EventArgs e)
        {
            if (writer == null)
            {
                MessageBox.Show("Vui lòng nhấn Kết nối Server trước!");
                return;
            }

            string user = textBox1.Text.Trim();
            string food = textBox2.Text.Trim();
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(food))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Họ tên và Tên món ăn!");
                return;
            }

            writer.WriteLine($"ADD|{user}|{food}|{selectedImageBase64}");
            MessageBox.Show("Thêm món ăn thành công!");
        }

        // button4: Random Cá nhân
        private void button4_Click(object sender, EventArgs e)
        {
            string myName = textBox1.Text.Trim();
            var myFoods = allFoods.Where(f => f.NguoiDongGop.Equals(myName, StringComparison.OrdinalIgnoreCase)).ToList();
            RandomFood(myFoods);
        }

        // button5: Random Cộng đồng
        private void button5_Click(object sender, EventArgs e)
        {
            RandomFood(allFoods);
        }

        private void RandomFood(List<FoodItem> list)
        {
            if (list.Count == 0)
            {
                MessageBox.Show("Không có món ăn nào trong danh sách phù hợp để random!");
                return;
            }

            Random rnd = new Random();
            FoodItem chosen = list[rnd.Next(list.Count)];

            string resultText = $"Món: {chosen.TenMon} (Của: {chosen.NguoiDongGop})";
            this.Text = resultText;

            if (!string.IsNullOrEmpty(chosen.Base64Image))
            {
                byte[] imgBytes = Convert.FromBase64String(chosen.Base64Image);
                using (MemoryStream ms = new MemoryStream(imgBytes))
                {
                    pictureBox2.Image = Image.FromStream(ms);
                }
            }
            else
            {
                pictureBox2.Image = null;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e) //họ và tên
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e) //tên món ăn
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e) //Khung xem trước ảnh khi chọn
        {
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void pictureBox2_Click(object sender, EventArgs e) //Khung hiện ảnh món ăn sau khi Random
        {
        }
    }

    public class FoodItem
    {
        public string ID { get; set; } = "";
        public string TenMon { get; set; } = "";
        public string NguoiDongGop { get; set; } = "";
        public string Base64Image { get; set; } = "";
    }
}