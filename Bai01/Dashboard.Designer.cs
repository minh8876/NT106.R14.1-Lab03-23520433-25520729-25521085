namespace Bai01
{
    partial class Dashboard
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnClient = new Button();
            btnServer = new Button();
            SuspendLayout();
            // 
            // btnClient
            // 
            btnClient.Location = new Point(265, 132);
            btnClient.Name = "btnClient";
            btnClient.Size = new Size(94, 29);
            btnClient.TabIndex = 0;
            btnClient.Text = "UDP Client";
            btnClient.UseVisualStyleBackColor = true;
            btnClient.Click += btnClient_Click;
            // 
            // btnServer
            // 
            btnServer.Location = new Point(429, 132);
            btnServer.Name = "btnServer";
            btnServer.Size = new Size(94, 29);
            btnServer.TabIndex = 1;
            btnServer.Text = "UDP Server";
            btnServer.UseVisualStyleBackColor = true;
            btnServer.Click += btnServer_Click;
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnServer);
            Controls.Add(btnClient);
            Name = "Dashboard";
            Text = "Dashboard";
            ResumeLayout(false);
        }

        #endregion

        private Button btnClient;
        private Button btnServer;
    }
}
