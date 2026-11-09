namespace Bai04
{
    partial class ClientApp
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
            txtCustomer = new TextBox();
            cboMovie = new ComboBox();
            cboRoom = new ComboBox();
            clbSeats = new CheckedListBox();
            btnBook = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // txtCustomer
            // 
            txtCustomer.Location = new Point(257, 24);
            txtCustomer.Name = "txtCustomer";
            txtCustomer.Size = new Size(151, 27);
            txtCustomer.TabIndex = 0;
            // 
            // cboMovie
            // 
            cboMovie.FormattingEnabled = true;
            cboMovie.Location = new Point(257, 88);
            cboMovie.Name = "cboMovie";
            cboMovie.Size = new Size(151, 28);
            cboMovie.TabIndex = 1;
            cboMovie.SelectedIndexChanged += cboMovie_SelectedIndexChanged;
            // 
            // cboRoom
            // 
            cboRoom.FormattingEnabled = true;
            cboRoom.Location = new Point(257, 156);
            cboRoom.Name = "cboRoom";
            cboRoom.Size = new Size(151, 28);
            cboRoom.TabIndex = 2;
            cboRoom.SelectedIndexChanged += cboRoom_SelectedIndexChanged;
            // 
            // clbSeats
            // 
            clbSeats.CheckOnClick = true;
            clbSeats.FormattingEnabled = true;
            clbSeats.Location = new Point(257, 225);
            clbSeats.MultiColumn = true;
            clbSeats.Name = "clbSeats";
            clbSeats.Size = new Size(150, 114);
            clbSeats.TabIndex = 3;
            clbSeats.ItemCheck += clbSeats_ItemCheck;
            // 
            // btnBook
            // 
            btnBook.Location = new Point(108, 366);
            btnBook.Name = "btnBook";
            btnBook.Size = new Size(299, 29);
            btnBook.TabIndex = 4;
            btnBook.Text = "Đặt vé";
            btnBook.UseVisualStyleBackColor = true;
            btnBook.Click += btnBook_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(108, 24);
            label1.Name = "label1";
            label1.Size = new Size(54, 20);
            label1.TabIndex = 5;
            label1.Text = "Họ tên";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(108, 91);
            label2.Name = "label2";
            label2.Size = new Size(42, 20);
            label2.TabIndex = 6;
            label2.Text = "Phim";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(108, 159);
            label3.Name = "label3";
            label3.Size = new Size(90, 20);
            label3.TabIndex = 7;
            label3.Text = "Phòng chiếu";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(108, 225);
            label4.Name = "label4";
            label4.Size = new Size(55, 20);
            label4.TabIndex = 8;
            label4.Text = "Số ghế";
            // 
            // ClientApp
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnBook);
            Controls.Add(clbSeats);
            Controls.Add(cboRoom);
            Controls.Add(cboMovie);
            Controls.Add(txtCustomer);
            Name = "ClientApp";
            Text = "ClientApp";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtCustomer;
        private ComboBox cboMovie;
        private ComboBox cboRoom;
        private CheckedListBox clbSeats;
        private Button btnBook;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}