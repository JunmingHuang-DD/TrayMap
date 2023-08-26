namespace Test
{
    partial class TagTestForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tray1 = new CommonUI.Tray();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.glassButton2 = new CommonUI.Buttons.GlassButton();
            this.glassButton1 = new CommonUI.Buttons.GlassButton();
            this.stackTray1 = new CommonUI.StackTray();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(2, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(704, 613);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tray1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(696, 587);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tray1
            // 
            this.tray1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tray1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tray1.CurrentOperation = ((CommonUI.Tray.OperationType)(((CommonUI.Tray.OperationType.Skip | CommonUI.Tray.OperationType.PickCandinate) 
            | CommonUI.Tray.OperationType.None)));
            this.tray1.IsShowCharacter = false;
            this.tray1.IsShowName = false;
            this.tray1.Location = new System.Drawing.Point(7, 24);
            this.tray1.Margin = new System.Windows.Forms.Padding(4);
            this.tray1.Name = "tray1";
            this.tray1.ShowCharaterMode = 0;
            this.tray1.Size = new System.Drawing.Size(657, 539);
            this.tray1.TabIndex = 0;
            this.tray1.TrayCurInfo = null;
            this.tray1.WaferSize = null;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.glassButton2);
            this.tabPage2.Controls.Add(this.glassButton1);
            this.tabPage2.Controls.Add(this.stackTray1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(696, 587);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // glassButton2
            // 
            this.glassButton2.Location = new System.Drawing.Point(241, 288);
            this.glassButton2.Name = "glassButton2";
            this.glassButton2.Size = new System.Drawing.Size(75, 23);
            this.glassButton2.TabIndex = 1;
            this.glassButton2.Text = "glassButton1";
            this.glassButton2.Click += new System.EventHandler(this.glassButton2_Click);
            // 
            // glassButton1
            // 
            this.glassButton1.Location = new System.Drawing.Point(241, 206);
            this.glassButton1.Name = "glassButton1";
            this.glassButton1.Size = new System.Drawing.Size(75, 23);
            this.glassButton1.TabIndex = 1;
            this.glassButton1.Text = "下一个";
            this.glassButton1.Click += new System.EventHandler(this.glassButton1_Click);
            // 
            // stackTray1
            // 
            this.stackTray1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.stackTray1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.stackTray1.CurrentOperation = ((CommonUI.Tray.OperationType)(((CommonUI.Tray.OperationType.Skip | CommonUI.Tray.OperationType.PickCandinate) 
            | CommonUI.Tray.OperationType.None)));
            this.stackTray1.IsShowCharacter = false;
            this.stackTray1.IsShowName = false;
            this.stackTray1.Location = new System.Drawing.Point(61, 28);
            this.stackTray1.Margin = new System.Windows.Forms.Padding(4);
            this.stackTray1.Name = "stackTray1";
            this.stackTray1.ShowCharaterMode = 0;
            this.stackTray1.Size = new System.Drawing.Size(71, 525);
            this.stackTray1.TabIndex = 0;
            this.stackTray1.TrayCurInfo = null;
            this.stackTray1.WaferSize = null;
            // 
            // TagTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 621);
            this.Controls.Add(this.tabControl1);
            this.Name = "TagTestForm";
            this.Text = "TagTestForm";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private CommonUI.Tray tray1;
        private System.Windows.Forms.TabPage tabPage2;
        private CommonUI.Buttons.GlassButton glassButton2;
        private CommonUI.Buttons.GlassButton glassButton1;
        private CommonUI.StackTray stackTray1;
    }
}