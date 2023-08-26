namespace RectBlock
{
    partial class Form1
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
            this.rectangleBlock1 = new CommonUI.RectangleBlock();
            this.glassButton1 = new CommonUI.Buttons.GlassButton();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rectangleBlock1
            // 
            this.rectangleBlock1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.rectangleBlock1.Location = new System.Drawing.Point(12, 48);
            this.rectangleBlock1.Name = "rectangleBlock1";
            this.rectangleBlock1.Size = new System.Drawing.Size(1346, 159);
            this.rectangleBlock1.TabIndex = 0;
            // 
            // glassButton1
            // 
            this.glassButton1.Location = new System.Drawing.Point(34, 225);
            this.glassButton1.Name = "glassButton1";
            this.glassButton1.Size = new System.Drawing.Size(113, 40);
            this.glassButton1.TabIndex = 1;
            this.glassButton1.Text = "获取结果";
            this.glassButton1.Click += new System.EventHandler(this.glassButton1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(210, 237);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 277);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.glassButton1);
            this.Controls.Add(this.rectangleBlock1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CommonUI.RectangleBlock rectangleBlock1;
        private CommonUI.Buttons.GlassButton glassButton1;
        private System.Windows.Forms.Label label1;
    }
}

