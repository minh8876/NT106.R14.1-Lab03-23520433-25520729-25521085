using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LAB3_Bai5
{
    public partial class ServerForm : Form
    {
        private TcpListener? listener;
        private List<TcpClient> clients = new List<TcpClient>();
        private string dbPath = "Data Source=FoodDB.sqlite;Version=3;";

        public ServerForm()
        {
            InitializeComponent();
            InitDatabase();
        }

        private void InitDatabase()
        {
            if (!File.Exists("FoodDB.sqlite"))
            {
                SQLiteConnection.CreateFile("FoodDB.sqlite");
                using (var conn = new SQLiteConnection(dbPath))
                {
                    conn.Open();
                    string sql = @"
                        CREATE TABLE NguoiDung (IDNCC INTEGER PRIMARY KEY AUTOINCREMENT, HoVaTen TEXT UNIQUE);
                        CREATE TABLE MonAn (IDMA INTEGER PRIMARY KEY AUTOINCREMENT, TenMonAn TEXT, HinhAnh TEXT, IDNCC INTEGER);";
                    new SQLiteCommand(sql, conn).ExecuteNonQuery();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread serverThread = new Thread(StartServer) { IsBackground = true };
            serverThread.Start();
            button1.Enabled = false;
            Log("Server đang lắng nghe tại port 8080...");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void StartServer()
        {
            listener = new TcpListener(IPAddress.Any, 8080);
            listener.Start();

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                lock (clients) { clients.Add(client); }
                Log($"Client mới kết nối: {client.Client.RemoteEndPoint}");

                Thread clientThread = new Thread(() => HandleClient(client)) { IsBackground = true };
                clientThread.Start();
            }
        }

        private void HandleClient(TcpClient client)
        {
            StreamReader reader = new StreamReader(client.GetStream(), Encoding.UTF8);
            try
            {
                SendFoodListToClient(client);

                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split('|');
                    if (parts[0] == "ADD" && parts.Length >= 4)
                    {
                        SaveFoodToDB(parts[1], parts[2], parts[3]);
                        Log($"{parts[1]} vừa thêm món: {parts[2]}");
                        BroadcastFoodList();
                    }
                }
            }
            catch { }
            finally
            {
                lock (clients) { clients.Remove(client); }
                client.Close();
            }
        }

        private void SaveFoodToDB(string user, string food, string img)
        {
            using (var conn = new SQLiteConnection(dbPath))
            {
                conn.Open();
                var cmdUser = new SQLiteCommand("INSERT OR IGNORE INTO NguoiDung(HoVaTen) VALUES(@u)", conn);
                cmdUser.Parameters.AddWithValue("@u", user);
                cmdUser.ExecuteNonQuery();

                var cmdGetId = new SQLiteCommand("SELECT IDNCC FROM NguoiDung WHERE HoVaTen=@u", conn);
                cmdGetId.Parameters.AddWithValue("@u", user);
                int idNcc = Convert.ToInt32(cmdGetId.ExecuteScalar());

                var cmdFood = new SQLiteCommand("INSERT INTO MonAn(TenMonAn, HinhAnh, IDNCC) VALUES(@f, @img, @id)", conn);
                cmdFood.Parameters.AddWithValue("@f", food);
                cmdFood.Parameters.AddWithValue("@img", img);
                cmdFood.Parameters.AddWithValue("@id", idNcc);
                cmdFood.ExecuteNonQuery();
            }
        }

        private string GetAllFoodsPayload()
        {
            List<string> items = new List<string>();
            using (var conn = new SQLiteConnection(dbPath))
            {
                conn.Open();
                string sql = @"SELECT m.IDMA, m.TenMonAn, n.HoVaTen, m.HinhAnh 
                               FROM MonAn m JOIN NguoiDung n ON m.IDNCC = n.IDNCC";
                using (var reader = new SQLiteCommand(sql, conn).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        items.Add($"{reader["IDMA"]};{reader["TenMonAn"]};{reader["HoVaTen"]};{reader["HinhAnh"]}");
                    }
                }
            }
            return "LIST|" + string.Join("#", items);
        }

        private void SendFoodListToClient(TcpClient client)
        {
            StreamWriter writer = new StreamWriter(client.GetStream(), Encoding.UTF8) { AutoFlush = true };
            writer.WriteLine(GetAllFoodsPayload());
        }

        private void BroadcastFoodList()
        {
            string payload = GetAllFoodsPayload();
            lock (clients)
            {
                foreach (var c in clients)
                {
                    try
                    {
                        StreamWriter writer = new StreamWriter(c.GetStream(), Encoding.UTF8) { AutoFlush = true };
                        writer.WriteLine(payload);
                    }
                    catch { }
                }
            }
        }

        private void Log(string msg)
        {
            if (InvokeRequired) { Invoke(new Action(() => Log(msg))); return; }
            listBox1.Items.Add(msg);
        }
    }
}