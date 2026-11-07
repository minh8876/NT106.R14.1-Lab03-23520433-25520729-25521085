namespace Bai01
{
    partial class UDPClient
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
            txtIP = new TextBox();
            textBox2 = new TextBox();
            rtbMessage = new RichTextBox();
            btnSend = new Button();
            label1 = new Label();
            txtPort = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // txtIP
            // 
            txtIP.Location = new Point(247, 47);
            txtIP.Name = "txtIP";
            txtIP.Size = new Size(125, 27);
            txtIP.TabIndex = 0;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(586, 47);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(125, 27);
            textBox2.TabIndex = 1;
            // 
            // rtbMessage
            // 
            rtbMessage.Location = new Point(247, 123);
            rtbMessage.Name = "rtbMessage";
            rtbMessage.Size = new Size(464, 192);
            rtbMessage.TabIndex = 2;
            rtbMessage.Text = "";
            // 
            // btnSend
            // 
            btnSend.Location = new Point(247, 341);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(94, 29);
            btnSend.TabIndex = 3;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(247, 24);
            label1.Name = "label1";
            label1.Size = new Size(112, 20);
            label1.TabIndex = 4;
            label1.Text = "IP Remote Host";
            // 
            // txtPort
            // 
            txtPort.AutoSize = true;
            txtPort.Location = new Point(586, 24);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(35, 20);
            txtPort.TabIndex = 5;
            txtPort.Text = "Port";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(247, 100);
            label3.Name = "label3";
            label3.Size = new Size(67, 20);
            label3.TabIndex = 6;
            label3.Text = "Message";
            // 
            // UDPClient
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label3);
            Controls.Add(txtPort);
            Controls.Add(label1);
            Controls.Add(btnSend);
            Controls.Add(rtbMessage);
            Controls.Add(textBox2);
            Controls.Add(txtIP);
            Name = "UDPClient";
            Text = "UDPClient";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtIP;
        private TextBox textBox2;
        private RichTextBox rtbMessage;
        private Button btnSend;
        private Label label1;
        private Label txtPort;
        private Label label3;
    }
}