namespace Bai01
{
    partial class UDPServer
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
            txtPort = new TextBox();
            btnListen = new Button();
            lvMessages = new ListView();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // txtPort
            // 
            txtPort.Location = new Point(158, 60);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(125, 27);
            txtPort.TabIndex = 0;
            // 
            // btnListen
            // 
            btnListen.Location = new Point(620, 60);
            btnListen.Name = "btnListen";
            btnListen.Size = new Size(94, 29);
            btnListen.TabIndex = 1;
            btnListen.Text = "Listen";
            btnListen.UseVisualStyleBackColor = true;
            btnListen.Click += btnListen_Click;
            // 
            // lvMessages
            // 
            lvMessages.Location = new Point(158, 141);
            lvMessages.Name = "lvMessages";
            lvMessages.Size = new Size(556, 245);
            lvMessages.TabIndex = 2;
            lvMessages.UseCompatibleStateImageBehavior = false;
            lvMessages.View = View.List;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(158, 37);
            label1.Name = "label1";
            label1.Size = new Size(35, 20);
            label1.TabIndex = 3;
            label1.Text = "Port";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(158, 118);
            label2.Name = "label2";
            label2.Size = new Size(137, 20);
            label2.TabIndex = 4;
            label2.Text = "Received messages";
            // 
            // UDPServer
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lvMessages);
            Controls.Add(btnListen);
            Controls.Add(txtPort);
            Name = "UDPServer";
            Text = "UDPServer";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtPort;
        private Button btnListen;
        private ListView lvMessages;
        private Label label1;
        private Label label2;
    }
}