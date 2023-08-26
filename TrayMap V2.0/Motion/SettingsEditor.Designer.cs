namespace Motion
{
    partial class SettingsEditor
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.button1.Location = new System.Drawing.Point(62, 33);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(164, 62);
            this.button1.TabIndex = 0;
            this.button1.Text = "生成Excel模板";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.button2.Location = new System.Drawing.Point(62, 134);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(164, 62);
            this.button2.TabIndex = 0;
            this.button2.Text = "加载配置并生成Excel文件";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.button3.Location = new System.Drawing.Point(62, 236);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(164, 62);
            this.button3.TabIndex = 0;
            this.button3.Text = "读取Excel并生成配置文件";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "Motion Setting.xlsx";
            this.openFileDialog1.Filter = "Excel File Name (*.xls, *.xlsx)|*.xls;*.xlsx";
            this.openFileDialog1.Title = "打开Excel配置文件";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "Motion.xml";
            this.openFileDialog2.Filter = "Motion Setting File (*.xml)|*.xml";
            this.openFileDialog2.Title = "打开轴配置文件";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "Motion Setting.xlsx";
            this.saveFileDialog1.Filter = "Excel File Name (*.xlsx)|*.xlsx";
            this.saveFileDialog1.Title = "保存Excel 文件";
            // 
            // saveFileDialog2
            // 
            this.saveFileDialog2.FileName = "Motion.xml";
            this.saveFileDialog2.Filter = "Motion Setting File (*.xml)|*.xml";
            this.saveFileDialog2.Title = "保存配置文件";
            // 
            // SettingsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 328);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsEditor";
            this.Text = "生成IO 轴的配置文件";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog2;
    }
}