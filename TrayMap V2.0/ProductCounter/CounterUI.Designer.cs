namespace IN3Automation.ProductCounter
{
    partial class CounterUI
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxTotalOutput = new System.Windows.Forms.TextBox();
            this.NG = new System.Windows.Forms.Label();
            this.textBoxNG = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxUPH = new System.Windows.Forms.TextBox();
            this.btnEmpty = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxYield = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(-4, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "总产量";
            // 
            // textBoxTotalOutput
            // 
            this.textBoxTotalOutput.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.textBoxTotalOutput.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxTotalOutput.Location = new System.Drawing.Point(55, 5);
            this.textBoxTotalOutput.Name = "textBoxTotalOutput";
            this.textBoxTotalOutput.ReadOnly = true;
            this.textBoxTotalOutput.Size = new System.Drawing.Size(63, 26);
            this.textBoxTotalOutput.TabIndex = 1;
            // 
            // NG
            // 
            this.NG.AutoSize = true;
            this.NG.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.NG.Location = new System.Drawing.Point(119, 10);
            this.NG.Name = "NG";
            this.NG.Size = new System.Drawing.Size(24, 16);
            this.NG.TabIndex = 0;
            this.NG.Text = "NG";
            // 
            // textBoxNG
            // 
            this.textBoxNG.BackColor = System.Drawing.Color.Red;
            this.textBoxNG.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxNG.Location = new System.Drawing.Point(143, 5);
            this.textBoxNG.Name = "textBoxNG";
            this.textBoxNG.ReadOnly = true;
            this.textBoxNG.Size = new System.Drawing.Size(49, 26);
            this.textBoxNG.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(287, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "UPH";
            // 
            // textBoxUPH
            // 
            this.textBoxUPH.BackColor = System.Drawing.Color.LightSalmon;
            this.textBoxUPH.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxUPH.Location = new System.Drawing.Point(320, 5);
            this.textBoxUPH.Name = "textBoxUPH";
            this.textBoxUPH.ReadOnly = true;
            this.textBoxUPH.Size = new System.Drawing.Size(54, 26);
            this.textBoxUPH.TabIndex = 1;
            // 
            // btnEmpty
            // 
            this.btnEmpty.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEmpty.Location = new System.Drawing.Point(380, 5);
            this.btnEmpty.Name = "btnEmpty";
            this.btnEmpty.Size = new System.Drawing.Size(61, 26);
            this.btnEmpty.TabIndex = 2;
            this.btnEmpty.Text = "清空";
            this.btnEmpty.UseVisualStyleBackColor = true;
            this.btnEmpty.Click += new System.EventHandler(this.btnEmpty_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(192, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "良率";
            // 
            // textBoxYield
            // 
            this.textBoxYield.BackColor = System.Drawing.Color.LightSalmon;
            this.textBoxYield.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxYield.Location = new System.Drawing.Point(233, 5);
            this.textBoxYield.Name = "textBoxYield";
            this.textBoxYield.ReadOnly = true;
            this.textBoxYield.Size = new System.Drawing.Size(54, 26);
            this.textBoxYield.TabIndex = 1;
            // 
            // CounterUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnEmpty);
            this.Controls.Add(this.textBoxYield);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxUPH);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxNG);
            this.Controls.Add(this.NG);
            this.Controls.Add(this.textBoxTotalOutput);
            this.Controls.Add(this.label1);
            this.Name = "CounterUI";
            this.Size = new System.Drawing.Size(447, 33);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxTotalOutput;
        private System.Windows.Forms.Label NG;
        private System.Windows.Forms.TextBox textBoxNG;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxUPH;
        private System.Windows.Forms.Button btnEmpty;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxYield;
    }
}
