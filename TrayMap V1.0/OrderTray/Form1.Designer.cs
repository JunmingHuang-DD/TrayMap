namespace OrderTray
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
            this.tray1 = new CommonUI.Tray();
            this.btnEditTray = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.labelInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tray1
            // 
            this.tray1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tray1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tray1.CurrentOperation = ((CommonUI.Tray.OperationType)(((CommonUI.Tray.OperationType.Skip | CommonUI.Tray.OperationType.PickCandinate) 
            | CommonUI.Tray.OperationType.NoneDefine)));
            this.tray1.IsAutoUpdata = false;
            this.tray1.IsShowCharacter = false;
            this.tray1.IsShowName = false;
            this.tray1.Location = new System.Drawing.Point(1, 0);
            this.tray1.Margin = new System.Windows.Forms.Padding(4);
            this.tray1.Name = "tray1";
            this.tray1.Size = new System.Drawing.Size(463, 448);
            this.tray1.TabIndex = 0;
            // 
            // btnEditTray
            // 
            this.btnEditTray.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEditTray.Location = new System.Drawing.Point(577, 28);
            this.btnEditTray.Name = "btnEditTray";
            this.btnEditTray.Size = new System.Drawing.Size(122, 36);
            this.btnEditTray.TabIndex = 2;
            this.btnEditTray.Text = "编辑Tray";
            this.btnEditTray.UseVisualStyleBackColor = true;
            this.btnEditTray.Click += new System.EventHandler(this.btnEditTray_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(588, 148);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 35);
            this.button1.TabIndex = 3;
            this.button1.Text = "初始化数据";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(588, 300);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(111, 35);
            this.button2.TabIndex = 3;
            this.button2.Text = "下一个点";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(600, 429);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(35, 12);
            this.labelInfo.TabIndex = 4;
            this.labelInfo.Text = "label";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnEditTray);
            this.Controls.Add(this.tray1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CommonUI.Tray tray1;
        private System.Windows.Forms.Button btnEditTray;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label labelInfo;
    }
}

