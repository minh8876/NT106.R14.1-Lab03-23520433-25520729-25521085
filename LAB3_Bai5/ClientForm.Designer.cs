namespace LAB3_Bai5
{
    partial class ClientForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBox1 = new TextBox();
            label1 = new Label();
            textBox2 = new TextBox();
            label2 = new Label();
            button1 = new Button();
            button4 = new Button();
            button5 = new Button();
            button2 = new Button();
            button3 = new Button();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            listView1 = new ListView();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(354, 109);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(200, 39);
            textBox1.TabIndex = 0;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(225, 109);
            label1.Name = "label1";
            label1.Size = new Size(123, 32);
            label1.TabIndex = 1;
            label1.Text = "Họ và tên:";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(354, 250);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(200, 39);
            textBox2.TabIndex = 2;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(202, 250);
            label2.Name = "label2";
            label2.Size = new Size(146, 32);
            label2.TabIndex = 3;
            label2.Text = "Tên món ăn:";
            // 
            // button1
            // 
            button1.Location = new Point(763, 95);
            button1.Name = "button1";
            button1.Size = new Size(252, 46);
            button1.TabIndex = 4;
            button1.Text = "Kết nối Server";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button4
            // 
            button4.Location = new Point(763, 288);
            button4.Name = "button4";
            button4.Size = new Size(252, 46);
            button4.TabIndex = 6;
            button4.Text = "Random Cá nhân";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(763, 361);
            button5.Name = "button5";
            button5.Size = new Size(252, 46);
            button5.TabIndex = 7;
            button5.Text = "Random Cộng đồng";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button2
            // 
            button2.Location = new Point(763, 161);
            button2.Name = "button2";
            button2.Size = new Size(252, 46);
            button2.TabIndex = 8;
            button2.Text = "Chọn ảnh...";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(763, 222);
            button3.Name = "button3";
            button3.Size = new Size(252, 46);
            button3.TabIndex = 9;
            button3.Text = "Thêm món ăn";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(1124, 203);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(200, 100);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 10;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(171, 489);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(307, 194);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 11;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // listView1
            // 
            listView1.Location = new Point(570, 489);
            listView1.Name = "listView1";
            listView1.Size = new Size(242, 194);
            listView1.TabIndex = 12;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(143, 442);
            label3.Name = "label3";
            label3.Size = new Size(335, 32);
            label3.TabIndex = 13;
            label3.Text = "Kết quả món ăn sẽ hiện ở đây";
            // 
            // ClientForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1574, 720);
            Controls.Add(label3);
            Controls.Add(listView1);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(textBox2);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Name = "ClientForm";
            Text = "ClientForm";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Label label1;
        private TextBox textBox2;
        private Label label2;
        private Button button1;
        private Button button4;
        private Button button5;
        private Button button2;
        private Button button3;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private ListView listView1;
        private Label label3;
    }
}