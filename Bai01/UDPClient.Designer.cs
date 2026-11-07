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
            txtPort = new TextBox();
            rtbMessage = new RichTextBox();
            btnSend = new Button();
            label1 = new Label();
            label2 = new Label();
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
            // txtPort
            // 
            txtPort.Location = new Point(586, 47);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(125, 27);
            txtPort.TabIndex = 1;
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
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(586, 24);
            label2.Name = "label2";
            label2.Size = new Size(35, 20);
            label2.TabIndex = 5;
            label2.Text = "Port";
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
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnSend);
            Controls.Add(rtbMessage);
            Controls.Add(txtPort);
            Controls.Add(txtIP);
            Name = "UDPClient";
            Text = "UDPClient";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtIP;
        private TextBox txtPort;
        private RichTextBox rtbMessage;
        private Button btnSend;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}