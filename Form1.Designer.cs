namespace Alarme
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.watchLb = new System.Windows.Forms.Label();
            this.txAcordar = new System.Windows.Forms.MaskedTextBox();
            this.BtnStart = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cbSoneca = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // watchLb
            // 
            this.watchLb.Dock = System.Windows.Forms.DockStyle.Top;
            this.watchLb.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.watchLb.ForeColor = System.Drawing.Color.MediumBlue;
            this.watchLb.Location = new System.Drawing.Point(0, 0);
            this.watchLb.Name = "watchLb";
            this.watchLb.Size = new System.Drawing.Size(210, 31);
            this.watchLb.TabIndex = 1;
            this.watchLb.Text = "Alarme";
            this.watchLb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txAcordar
            // 
            this.txAcordar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txAcordar.Location = new System.Drawing.Point(71, 34);
            this.txAcordar.Mask = "00:00:00";
            this.txAcordar.Name = "txAcordar";
            this.txAcordar.Size = new System.Drawing.Size(78, 26);
            this.txAcordar.TabIndex = 6;
            this.txAcordar.TabStop = false;
            this.txAcordar.Leave += new System.EventHandler(this.txAcordar_Leave);
            // 
            // BtnStart
            // 
            this.BtnStart.Enabled = false;
            this.BtnStart.Image = ((System.Drawing.Image)(resources.GetObject("BtnStart.Image")));
            this.BtnStart.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.BtnStart.Location = new System.Drawing.Point(31, 66);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(150, 41);
            this.BtnStart.TabIndex = 7;
            this.BtnStart.Text = "Soneca";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cbSoneca
            // 
            this.cbSoneca.FormattingEnabled = true;
            this.cbSoneca.Items.AddRange(new object[] {
            "1 minuto",
            "5 minutos",
            "10 minutos",
            "15 minutos",
            "20 minutos ",
            "30 minutos",
            "1 hora"});
            this.cbSoneca.Location = new System.Drawing.Point(42, 113);
            this.cbSoneca.Name = "cbSoneca";
            this.cbSoneca.Size = new System.Drawing.Size(121, 21);
            this.cbSoneca.TabIndex = 8;
            this.cbSoneca.SelectedIndexChanged += new System.EventHandler(this.cbSoneca_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(210, 146);
            this.Controls.Add(this.cbSoneca);
            this.Controls.Add(this.BtnStart);
            this.Controls.Add(this.txAcordar);
            this.Controls.Add(this.watchLb);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alarme";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label watchLb;
        private System.Windows.Forms.MaskedTextBox txAcordar;
        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox cbSoneca;
    }
}

