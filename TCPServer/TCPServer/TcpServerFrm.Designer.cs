namespace TCPServer
{
    partial class TcpServerFrm
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
            this.startBtn = new System.Windows.Forms.Button();
            this.stopBtn = new System.Windows.Forms.Button();
            this.statusLb = new System.Windows.Forms.Label();
            this.logRtb = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(12, 12);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(92, 33);
            this.startBtn.TabIndex = 0;
            this.startBtn.Text = "Start";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // stopBtn
            // 
            this.stopBtn.Location = new System.Drawing.Point(480, 12);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(92, 33);
            this.stopBtn.TabIndex = 1;
            this.stopBtn.Text = "Stop";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // statusLb
            // 
            this.statusLb.Location = new System.Drawing.Point(12, 48);
            this.statusLb.Name = "statusLb";
            this.statusLb.Size = new System.Drawing.Size(560, 31);
            this.statusLb.TabIndex = 2;
            this.statusLb.Text = "Status: IP:Port";
            this.statusLb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // logRtb
            // 
            this.logRtb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.logRtb.DetectUrls = false;
            this.logRtb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logRtb.Location = new System.Drawing.Point(12, 82);
            this.logRtb.Name = "logRtb";
            this.logRtb.ReadOnly = true;
            this.logRtb.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.logRtb.ShortcutsEnabled = false;
            this.logRtb.Size = new System.Drawing.Size(560, 167);
            this.logRtb.TabIndex = 3;
            this.logRtb.TabStop = false;
            this.logRtb.Text = "";
            // 
            // TcpServerFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 261);
            this.Controls.Add(this.logRtb);
            this.Controls.Add(this.statusLb);
            this.Controls.Add(this.stopBtn);
            this.Controls.Add(this.startBtn);
            this.Name = "TcpServerFrm";
            this.Text = "TCPServer";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.RichTextBox logRtb;

        private System.Windows.Forms.Label statusLb;

        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Button stopBtn;

        #endregion
    }
}