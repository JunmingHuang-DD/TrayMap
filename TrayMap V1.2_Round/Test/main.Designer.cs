namespace Test
{
    partial class main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnEditTray = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.roundWafer1 = new CommonUI.RoundWafer();
            this.SuspendLayout();
            // 
            // btnEditTray
            // 
            this.btnEditTray.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEditTray.Location = new System.Drawing.Point(524, 36);
            this.btnEditTray.Name = "btnEditTray";
            this.btnEditTray.Size = new System.Drawing.Size(122, 36);
            this.btnEditTray.TabIndex = 1;
            this.btnEditTray.Text = "编辑Tray";
            this.btnEditTray.UseVisualStyleBackColor = true;
            this.btnEditTray.Click += new System.EventHandler(this.btnEditTray_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(524, 347);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(320, 129);
            this.textBox1.TabIndex = 7;
            // 
            // roundWafer1
            // 
            this.roundWafer1.BackColor = System.Drawing.Color.White;
            this.roundWafer1.Location = new System.Drawing.Point(12, 12);
            this.roundWafer1.Name = "roundWafer1";
            this.roundWafer1.Size = new System.Drawing.Size(350, 350);
            this.roundWafer1.TabIndex = 8;
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 556);
            this.Controls.Add(this.roundWafer1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnEditTray);
            this.DoubleBuffered = true;
            this.Name = "main";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnEditTray;
        private System.Windows.Forms.TextBox textBox1;
        private CommonUI.RoundWafer roundWafer1;
    }
}

