namespace HalconTest
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonOpen = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonTrigger = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.halconView1 = new VisionHalcon.HalconView();
            this.buttonLens = new System.Windows.Forms.Button();
            this.buttonLabel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonOpen
            // 
            this.buttonOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOpen.Location = new System.Drawing.Point(636, 13);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(107, 28);
            this.buttonOpen.TabIndex = 1;
            this.buttonOpen.Text = "Open Image";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Image File (*.png;*.bmp)|*.png;*.bmp";
            this.openFileDialog1.Title = "Open Image File";
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(655, 93);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(84, 16);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "show cross";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // buttonConnect
            // 
            this.buttonConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonConnect.Location = new System.Drawing.Point(624, 152);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(119, 38);
            this.buttonConnect.TabIndex = 4;
            this.buttonConnect.Text = "Connect Camera";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonTrigger
            // 
            this.buttonTrigger.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTrigger.Location = new System.Drawing.Point(624, 214);
            this.buttonTrigger.Name = "buttonTrigger";
            this.buttonTrigger.Size = new System.Drawing.Size(119, 38);
            this.buttonTrigger.TabIndex = 4;
            this.buttonTrigger.Text = "Hardware Trigger";
            this.buttonTrigger.UseVisualStyleBackColor = true;
            this.buttonTrigger.Click += new System.EventHandler(this.buttonTrigger_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(636, 47);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(107, 28);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Save Image";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // halconView1
            // 
            this.halconView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.halconView1.Location = new System.Drawing.Point(7, 12);
            this.halconView1.Name = "halconView1";
            this.halconView1.Size = new System.Drawing.Size(602, 454);
            this.halconView1.TabIndex = 2;
            // 
            // buttonLens
            // 
            this.buttonLens.Location = new System.Drawing.Point(636, 298);
            this.buttonLens.Name = "buttonLens";
            this.buttonLens.Size = new System.Drawing.Size(102, 38);
            this.buttonLens.TabIndex = 5;
            this.buttonLens.Text = "Process Lens";
            this.buttonLens.UseVisualStyleBackColor = true;
            this.buttonLens.Click += new System.EventHandler(this.buttonLens_Click);
            // 
            // buttonLabel
            // 
            this.buttonLabel.Location = new System.Drawing.Point(636, 367);
            this.buttonLabel.Name = "buttonLabel";
            this.buttonLabel.Size = new System.Drawing.Size(102, 38);
            this.buttonLabel.TabIndex = 5;
            this.buttonLabel.Text = "Process Label";
            this.buttonLabel.UseVisualStyleBackColor = true;
            this.buttonLabel.Click += new System.EventHandler(this.buttonLabel_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 483);
            this.Controls.Add(this.buttonLabel);
            this.Controls.Add(this.buttonLens);
            this.Controls.Add(this.buttonTrigger);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.halconView1);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonOpen);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private VisionHalcon.HalconView halconView1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonTrigger;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonLens;
        private System.Windows.Forms.Button buttonLabel;
    }
}

