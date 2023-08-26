namespace CommonUI
{
    partial class Tray
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tray));
            this.labelPos = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelIndex = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.选择开始ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.选择跳过ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全部跳过ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重置缩放比例ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelPos
            // 
            resources.ApplyResources(this.labelPos, "labelPos");
            this.labelPos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.labelPos.Name = "labelPos";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel2.Controls.Add(this.labelIndex);
            this.panel2.Controls.Add(this.labelName);
            this.panel2.Controls.Add(this.labelPos);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // labelIndex
            // 
            resources.ApplyResources(this.labelIndex, "labelIndex");
            this.labelIndex.ForeColor = System.Drawing.Color.White;
            this.labelIndex.Name = "labelIndex";
            // 
            // labelName
            // 
            resources.ApplyResources(this.labelName, "labelName");
            this.labelName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.labelName.Name = "labelName";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.ContextMenuStrip = this.contextMenuStrip1;
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            this.panel1.Click += new System.EventHandler(this.Tray_Click);
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.Tray_Paint);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Tray_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.选择开始ToolStripMenuItem,
            this.选择跳过ToolStripMenuItem,
            this.全部跳过ToolStripMenuItem,
            this.重置ToolStripMenuItem,
            this.重置缩放比例ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.TabStop = true;
            // 
            // 选择开始ToolStripMenuItem
            // 
            this.选择开始ToolStripMenuItem.Name = "选择开始ToolStripMenuItem";
            resources.ApplyResources(this.选择开始ToolStripMenuItem, "选择开始ToolStripMenuItem");
            this.选择开始ToolStripMenuItem.Click += new System.EventHandler(this.选择开始ToolStripMenuItem_Click);
            // 
            // 选择跳过ToolStripMenuItem
            // 
            this.选择跳过ToolStripMenuItem.Name = "选择跳过ToolStripMenuItem";
            resources.ApplyResources(this.选择跳过ToolStripMenuItem, "选择跳过ToolStripMenuItem");
            this.选择跳过ToolStripMenuItem.Click += new System.EventHandler(this.选择跳过ToolStripMenuItem_Click);
            // 
            // 全部跳过ToolStripMenuItem
            // 
            this.全部跳过ToolStripMenuItem.Name = "全部跳过ToolStripMenuItem";
            resources.ApplyResources(this.全部跳过ToolStripMenuItem, "全部跳过ToolStripMenuItem");
            this.全部跳过ToolStripMenuItem.Click += new System.EventHandler(this.全部跳过ToolStripMenuItem_Click);
            // 
            // 重置ToolStripMenuItem
            // 
            this.重置ToolStripMenuItem.Name = "重置ToolStripMenuItem";
            resources.ApplyResources(this.重置ToolStripMenuItem, "重置ToolStripMenuItem");
            this.重置ToolStripMenuItem.Click += new System.EventHandler(this.重置ToolStripMenuItem_Click);
            // 
            // 重置缩放比例ToolStripMenuItem
            // 
            this.重置缩放比例ToolStripMenuItem.Name = "重置缩放比例ToolStripMenuItem";
            resources.ApplyResources(this.重置缩放比例ToolStripMenuItem, "重置缩放比例ToolStripMenuItem");
            this.重置缩放比例ToolStripMenuItem.Click += new System.EventHandler(this.重置缩放比例ToolStripMenuItem_Click);
            // 
            // Tray
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            resources.ApplyResources(this, "$this");
            this.Name = "Tray";
            this.Load += new System.EventHandler(this.Tray_Load);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.TrayMouseWheel);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        protected System.Windows.Forms.Panel panel2;
        protected System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem 选择开始ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 选择跳过ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全部跳过ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重置ToolStripMenuItem;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        public System.Windows.Forms.Label labelName;
        private System.Windows.Forms.ToolStripMenuItem 重置缩放比例ToolStripMenuItem;
        public System.Windows.Forms.Label labelPos;
        public System.Windows.Forms.Label labelIndex;
    }
}
