using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai04
{
    public partial class ServerApp : Form
    {
        private TcpListener server;
        private List<TcpClient> clients = new List<TcpClient>();
        private readonly object lockObj = new object();

        public class Movie
        {
            public string Name { get; set; }
            public double BasePrice { get; set; }
            public List<int> Rooms { get; set; } = new List<int>();
            public int SoldCount { get; set; } = 0;
            public double Revenue { get; set; } = 0;
        }

        private Dictionary<string, Movie> movies = new Dictionary<string, Movie>();
        private HashSet<string> soldSeats = new HashSet<string>();

        private HashSet<string> votSeats = new HashSet<string> { "A1", "A5", "C1", "C5" };
        private HashSet<string> vipSeats = new HashSet<string> { "B2", "B3", "B4" };

        public bool IsReady { get; private set; } = false;

        public ServerApp()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            LoadInputData("input.txt");
        }
        private void LoadInputData(string filePath)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Không tìm thấy file input.txt!");
                IsReady = false;
                return;
            }

            string[] lines = File.ReadAllLines(filePath);
            for (int i = 0; i < lines.Length; i += 3)
            {
                if (i + 2 >= lines.Length) break;
                Movie m = new Movie
                {
                    Name = lines[i].Trim(),
                    BasePrice = double.Parse(lines[i + 1].Trim()),
                    Rooms = lines[i + 2].Split(',').Select(int.Parse).ToList()
                };
                movies[m.Name] = m;
            }
            Log($"Đã nạp {movies.Count} phim từ file.");
            IsReady = true;
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            server = new TcpListener(IPAddress.Any, 8888);
            server.Start();
            btnListen.Enabled = false;
            Log("Server đang lắng nghe tại port 8888...");

            Thread acceptThread = new Thread(AcceptClients) { IsBackground = true };
            acceptThread.Start();
        }

        private void AcceptClients()
        {
            while (true)
            {
                try
                {
                    TcpClient client = server.AcceptTcpClient();
                    lock (lockObj) { clients.Add(client); }
                    Log("Một quầy (Client) mới đã kết nối!");

                    SendInitData(client);

                    Thread clientThread = new Thread(() => HandleClient(client)) { IsBackground = true };
                    clientThread.Start();
                }
                catch { break; }
            }
        }

        private void SendInitData(TcpClient client)
        {
            List<string> movieStrs = new List<string>();
            foreach (var m in movies.Values)
            {
                movieStrs.Add($"{m.Name};{m.BasePrice};{string.Join(",", m.Rooms)}");
            }
            SendMessage(client, "INIT|" + string.Join("#", movieStrs));

            lock (lockObj)
            {
                if (soldSeats.Count > 0)
                    SendMessage(client, "UPDATE|" + string.Join(",", soldSeats));
            }
        }

        private void HandleClient(TcpClient client)
        {
            StreamReader reader = new StreamReader(client.GetStream(), Encoding.UTF8);
            try
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split('|');
                    if (parts[0] == "BOOK")
                    {
                        string customer = parts[1];
                        string movieName = parts[2];
                        int room = int.Parse(parts[3]);
                        string[] seats = parts[4].Split(',');

                        ProcessBooking(client, customer, movieName, room, seats);
                    }
                }
            }
            catch
            {
                Log("Một Client đã ngắt kết nối.");
            }
            finally
            {
                lock (lockObj) { clients.Remove(client); }
                client.Close();
            }
        }

        private void ProcessBooking(TcpClient client, string customer, string movieName, int room, string[] seats)
        {
            lock (lockObj)
            {
                foreach (string s in seats)
                {
                    string key = $"{room}-{s.Trim()}";
                    if (soldSeats.Contains(key))
                    {
                        SendMessage(client, $"RESULT|FAIL|Ghế {s} tại phòng {room} đã có người đặt!");
                        return;
                    }
                }

                Movie movie = movies[movieName];
                double totalBill = 0;
                List<string> newlySold = new List<string>();

                foreach (string s in seats)
                {
                    string cleanSeat = s.Trim();
                    soldSeats.Add($"{room}-{cleanSeat}");
                    newlySold.Add($"{room}-{cleanSeat}");
                    totalBill += GetSeatPrice(cleanSeat, movie.BasePrice);
                }

                movie.SoldCount += seats.Length;
                movie.Revenue += totalBill;

                string receipt = $"Khách hàng: {customer} - Phim: {movieName} - Phòng: {room} - Ghế: {string.Join(", ", seats)} - Tổng tiền: {totalBill:N0}đ";
                Log($"Đặt thành công: {receipt}");

                Broadcast("UPDATE|" + string.Join(",", newlySold));
                SendMessage(client, $"RESULT|OK|{receipt}");
            }
        }

        private double GetSeatPrice(string seat, double basePrice)
        {
            if (votSeats.Contains(seat)) return basePrice * 0.25;
            if (vipSeats.Contains(seat)) return basePrice * 2.0;
            return basePrice * 1.0;
        }

        private void SendMessage(TcpClient client, string msg)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(msg + "\r\n");
                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);
                stream.Flush();
            }
            catch { }
        }

        private void Broadcast(string msg)
        {
            foreach (var c in clients.ToList())
            {
                SendMessage(c, msg);
            }
        }

        private async void btnExport_Click(object sender, EventArgs e)
        {
            btnExport.Enabled = false;
            progressBar1.Value = 0;

            var sortedMovies = movies.Values.OrderByDescending(m => m.Revenue).ToList();
            int totalMovies = sortedMovies.Count;
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < totalMovies; i++)
            {
                var m = sortedMovies[i];
                int totalCapacity = m.Rooms.Count * 15;
                int remain = totalCapacity - m.SoldCount;
                double rate = totalCapacity > 0 ? (double)m.SoldCount / totalCapacity * 100 : 0;

                sb.AppendLine("Tên phim: " + m.Name);
                sb.AppendLine("Số vé bán ra: " + m.SoldCount);
                sb.AppendLine("Số vé tồn: " + remain);
                sb.AppendLine("Tỉ lệ vé bán ra: " + rate.ToString("0.00") + "%");
                sb.AppendLine("Doanh thu: " + m.Revenue);
                sb.AppendLine("Xếp hạng doanh thu: " + (i + 1));
                sb.AppendLine();

                progressBar1.Value = (int)((i + 1.0) / totalMovies * 100);
                await Task.Delay(300);
            }

            File.WriteAllText("output.txt", sb.ToString(), Encoding.UTF8);
            MessageBox.Show("Đã xuất thống kê ra file output.txt thành công!");
            btnExport.Enabled = true;
        }

        private void Log(string text)
        {
            rtbLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {text}\n");
        }

        private void ServerApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                server?.Stop();
                lock (lockObj)
                {
                    foreach (var c in clients) c.Close();
                    clients.Clear();
                }
            }
            catch { }
        }
    }
}
