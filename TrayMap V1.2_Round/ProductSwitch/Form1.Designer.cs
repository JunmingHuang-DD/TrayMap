namespace ProductSwitch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btn_Delete = new CommonUI.Buttons.GlassButton();
            this.btnRefresh = new CommonUI.Buttons.GlassButton();
            this.buttonOK = new CommonUI.Buttons.GlassButton();
            this.btnCancel = new CommonUI.Buttons.GlassButton();
            this.btnKill = new CommonUI.Buttons.GlassButton();
            this.btnSaveCurrent = new CommonUI.Buttons.GlassButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnAdd = new CommonUI.Buttons.GlassButton();
            this.btnAddRefresh = new CommonUI.Buttons.GlassButton();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox_VisionList = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.textBox_ProductSwitchName = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(18, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "可选型号列表：";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("宋体", 15.75F);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(183, 129);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(241, 29);
            this.comboBox1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(180, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "配方型号选择";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(18, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(164, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "当前产品型号：";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Font = new System.Drawing.Font("宋体", 15.5F);
            this.textBox1.Location = new System.Drawing.Point(183, 65);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(240, 31);
            this.textBox1.TabIndex = 5;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(453, 342);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.BurlyWood;
            this.tabPage1.Controls.Add(this.btn_Delete);
            this.tabPage1.Controls.Add(this.btnRefresh);
            this.tabPage1.Controls.Add(this.buttonOK);
            this.tabPage1.Controls.Add(this.btnCancel);
            this.tabPage1.Controls.Add(this.btnKill);
            this.tabPage1.Controls.Add(this.btnSaveCurrent);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.comboBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(445, 316);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "切换型号";
            // 
            // btn_Delete
            // 
            this.btn_Delete.BackColor = System.Drawing.Color.Red;
            this.btn_Delete.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Delete.ForeColor = System.Drawing.Color.Indigo;
            this.btn_Delete.InnerBorderColor = System.Drawing.Color.White;
            this.btn_Delete.Location = new System.Drawing.Point(32, 183);
            this.btn_Delete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(132, 40);
            this.btn_Delete.TabIndex = 14;
            this.btn_Delete.Tag = "L";
            this.btn_Delete.Text = "删除已有型号";
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.DarkOrchid;
            this.btnRefresh.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRefresh.ForeColor = System.Drawing.Color.Indigo;
            this.btnRefresh.InnerBorderColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(294, 183);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(129, 39);
            this.btnRefresh.TabIndex = 13;
            this.btnRefresh.Tag = "Up";
            this.btnRefresh.Text = "刷新当前列表";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click_1);
            // 
            // buttonOK
            // 
            this.buttonOK.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonOK.ForeColor = System.Drawing.Color.Cyan;
            this.buttonOK.InnerBorderColor = System.Drawing.Color.White;
            this.buttonOK.Location = new System.Drawing.Point(291, 328);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(132, 40);
            this.buttonOK.TabIndex = 12;
            this.buttonOK.Tag = "L";
            this.buttonOK.Text = "确定切换";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click_1);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.ForeColor = System.Drawing.Color.Cyan;
            this.btnCancel.InnerBorderColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(32, 257);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(132, 40);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Tag = "L";
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click_1);
            // 
            // btnKill
            // 
            this.btnKill.BackColor = System.Drawing.Color.Crimson;
            this.btnKill.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnKill.ForeColor = System.Drawing.Color.Indigo;
            this.btnKill.InnerBorderColor = System.Drawing.Color.White;
            this.btnKill.Location = new System.Drawing.Point(32, 328);
            this.btnKill.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnKill.Name = "btnKill";
            this.btnKill.Size = new System.Drawing.Size(132, 40);
            this.btnKill.TabIndex = 10;
            this.btnKill.Tag = "L";
            this.btnKill.Text = "关闭贴膜机程序";
            this.btnKill.Click += new System.EventHandler(this.btnKill_Click_1);
            // 
            // btnSaveCurrent
            // 
            this.btnSaveCurrent.BackColor = System.Drawing.Color.LimeGreen;
            this.btnSaveCurrent.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSaveCurrent.ForeColor = System.Drawing.Color.Indigo;
            this.btnSaveCurrent.InnerBorderColor = System.Drawing.Color.White;
            this.btnSaveCurrent.Location = new System.Drawing.Point(291, 257);
            this.btnSaveCurrent.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSaveCurrent.Name = "btnSaveCurrent";
            this.btnSaveCurrent.Size = new System.Drawing.Size(132, 40);
            this.btnSaveCurrent.TabIndex = 9;
            this.btnSaveCurrent.Tag = "L";
            this.btnSaveCurrent.Text = "保存当前产品";
            this.btnSaveCurrent.Click += new System.EventHandler(this.btnSaveCurrent_Click_1);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.BurlyWood;
            this.tabPage2.Controls.Add(this.btnAdd);
            this.tabPage2.Controls.Add(this.btnAddRefresh);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.comboBox_VisionList);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label23);
            this.tabPage2.Controls.Add(this.label24);
            this.tabPage2.Controls.Add(this.comboBox2);
            this.tabPage2.Controls.Add(this.textBox_ProductSwitchName);
            this.tabPage2.Controls.Add(this.textBox2);
            this.tabPage2.Controls.Add(this.label25);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(445, 316);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "添加新型号";
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Blue;
            this.btnAdd.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAdd.ForeColor = System.Drawing.Color.Lavender;
            this.btnAdd.InnerBorderColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(246, 239);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(117, 50);
            this.btnAdd.TabIndex = 27;
            this.btnAdd.Tag = "Up";
            this.btnAdd.Text = "确定添加";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click_1);
            // 
            // btnAddRefresh
            // 
            this.btnAddRefresh.BackColor = System.Drawing.Color.DarkOrchid;
            this.btnAddRefresh.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAddRefresh.ForeColor = System.Drawing.Color.Indigo;
            this.btnAddRefresh.InnerBorderColor = System.Drawing.Color.White;
            this.btnAddRefresh.Location = new System.Drawing.Point(92, 239);
            this.btnAddRefresh.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAddRefresh.Name = "btnAddRefresh";
            this.btnAddRefresh.Size = new System.Drawing.Size(117, 50);
            this.btnAddRefresh.TabIndex = 26;
            this.btnAddRefresh.Tag = "Up";
            this.btnAddRefresh.Text = "刷新当前列表";
            this.btnAddRefresh.Click += new System.EventHandler(this.btnRefresh_Click_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(19, 356);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(164, 21);
            this.label5.TabIndex = 24;
            this.label5.Text = "视觉类型列表：";
            this.label5.Visible = false;
            // 
            // comboBox_VisionList
            // 
            this.comboBox_VisionList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_VisionList.Font = new System.Drawing.Font("宋体", 15.75F);
            this.comboBox_VisionList.FormattingEnabled = true;
            this.comboBox_VisionList.Location = new System.Drawing.Point(184, 354);
            this.comboBox_VisionList.Name = "comboBox_VisionList";
            this.comboBox_VisionList.Size = new System.Drawing.Size(241, 29);
            this.comboBox_VisionList.TabIndex = 25;
            this.comboBox_VisionList.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(191, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 21);
            this.label4.TabIndex = 22;
            this.label4.Text = "添加新配方";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label23.ForeColor = System.Drawing.Color.White;
            this.label23.Location = new System.Drawing.Point(20, 68);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(164, 21);
            this.label23.TabIndex = 14;
            this.label23.Text = "当前产品型号：";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label24.ForeColor = System.Drawing.Color.White;
            this.label24.Location = new System.Drawing.Point(20, 118);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(164, 21);
            this.label24.TabIndex = 15;
            this.label24.Text = "可选模板列表：";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.Font = new System.Drawing.Font("宋体", 15.75F);
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(185, 116);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(241, 29);
            this.comboBox2.TabIndex = 16;
            // 
            // textBox_ProductSwitchName
            // 
            this.textBox_ProductSwitchName.BackColor = System.Drawing.Color.White;
            this.textBox_ProductSwitchName.Font = new System.Drawing.Font("宋体", 15.5F);
            this.textBox_ProductSwitchName.Location = new System.Drawing.Point(186, 176);
            this.textBox_ProductSwitchName.Name = "textBox_ProductSwitchName";
            this.textBox_ProductSwitchName.Size = new System.Drawing.Size(240, 31);
            this.textBox_ProductSwitchName.TabIndex = 19;
            this.textBox_ProductSwitchName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.White;
            this.textBox2.Font = new System.Drawing.Font("宋体", 15.5F);
            this.textBox2.Location = new System.Drawing.Point(185, 65);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(240, 31);
            this.textBox2.TabIndex = 17;
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label25.ForeColor = System.Drawing.Color.White;
            this.label25.Location = new System.Drawing.Point(21, 179);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(164, 21);
            this.label25.TabIndex = 18;
            this.label25.Text = "添加新型号名：";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 342);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "配方型号更新";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.TextBox textBox_ProductSwitchName;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox_VisionList;
        private CommonUI.Buttons.GlassButton btnSaveCurrent;
        private CommonUI.Buttons.GlassButton btnKill;
        private CommonUI.Buttons.GlassButton buttonOK;
        private CommonUI.Buttons.GlassButton btnCancel;
        private CommonUI.Buttons.GlassButton btnRefresh;
        private CommonUI.Buttons.GlassButton btnAdd;
        private CommonUI.Buttons.GlassButton btnAddRefresh;
        private CommonUI.Buttons.GlassButton btn_Delete;
    }
}

