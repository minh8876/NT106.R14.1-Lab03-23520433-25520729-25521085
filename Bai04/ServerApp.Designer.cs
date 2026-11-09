namespace Bai04
{
    partial class ServerApp
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
            btnExport = new Button();
            btnListen = new Button();
            progressBar1 = new ProgressBar();
            rtbLog = new RichTextBox();
            SuspendLayout();
            // 
            // btnExport
            // 
            btnExport.Location = new Point(156, 352);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(94, 29);
            btnExport.TabIndex = 0;
            btnExport.Text = "Export";
            btnExport.UseVisualStyleBackColor = true;
            // 
            // btnListen
            // 
            btnListen.Location = new Point(156, 66);
            btnListen.Name = "btnListen";
            btnListen.Size = new Size(94, 29);
            btnListen.TabIndex = 1;
            btnListen.Text = "Listen";
            btnListen.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(288, 352);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(412, 29);
            progressBar1.TabIndex = 2;
            // 
            // rtbLog
            // 
            rtbLog.Location = new Point(288, 67);
            rtbLog.Name = "rtbLog";
            rtbLog.ReadOnly = true;
            rtbLog.Size = new Size(412, 262);
            rtbLog.TabIndex = 3;
            rtbLog.Text = "";
            // 
            // ServerApp
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(rtbLog);
            Controls.Add(progressBar1);
            Controls.Add(btnListen);
            Controls.Add(btnExport);
            Name = "ServerApp";
            Text = "ServerApp";
            ResumeLayout(false);
        }

        #endregion

        private Button btnExport;
        private Button btnListen;
        private ProgressBar progressBar1;
        private RichTextBox rtbLog;
    }
}
