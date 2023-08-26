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
            this.panel1 = new System.Windows.Forms.Panel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.选择开始ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.选择跳过ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.全部跳过ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.重置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置LabelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.ContextMenuStrip = this.contextMenuStrip1;
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            this.panel1.Click += new System.EventHandler(this.Tray_Click);
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.Tray_Paint);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Tray_MouseMove);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置LabelToolStripMenuItem,
            this.toolStripSeparator4,
            this.选择开始ToolStripMenuItem,
            this.toolStripSeparator1,
            this.选择跳过ToolStripMenuItem,
            this.toolStripSeparator2,
            this.全部跳过ToolStripMenuItem,
            this.toolStripSeparator3,
            this.重置ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            // 
            // 选择开始ToolStripMenuItem
            // 
            this.选择开始ToolStripMenuItem.Name = "选择开始ToolStripMenuItem";
            resources.ApplyResources(this.选择开始ToolStripMenuItem, "选择开始ToolStripMenuItem");
            this.选择开始ToolStripMenuItem.Tag = "Start";
            this.选择开始ToolStripMenuItem.Click += new System.EventHandler(this.选择开始ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // 选择跳过ToolStripMenuItem
            // 
            this.选择跳过ToolStripMenuItem.Name = "选择跳过ToolStripMenuItem";
            resources.ApplyResources(this.选择跳过ToolStripMenuItem, "选择跳过ToolStripMenuItem");
            this.选择跳过ToolStripMenuItem.Tag = "Skip";
            this.选择跳过ToolStripMenuItem.Click += new System.EventHandler(this.选择跳过ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // 全部跳过ToolStripMenuItem
            // 
            this.全部跳过ToolStripMenuItem.Name = "全部跳过ToolStripMenuItem";
            resources.ApplyResources(this.全部跳过ToolStripMenuItem, "全部跳过ToolStripMenuItem");
            this.全部跳过ToolStripMenuItem.Tag = "SkipAll";
            this.全部跳过ToolStripMenuItem.Click += new System.EventHandler(this.全部跳过ToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // 重置ToolStripMenuItem
            // 
            this.重置ToolStripMenuItem.Name = "重置ToolStripMenuItem";
            resources.ApplyResources(this.重置ToolStripMenuItem, "重置ToolStripMenuItem");
            this.重置ToolStripMenuItem.Tag = "Reset";
            this.重置ToolStripMenuItem.Click += new System.EventHandler(this.重置ToolStripMenuItem_Click);
            // 
            // 设置LabelToolStripMenuItem
            // 
            this.设置LabelToolStripMenuItem.Name = "设置LabelToolStripMenuItem";
            resources.ApplyResources(this.设置LabelToolStripMenuItem, "设置LabelToolStripMenuItem");
            this.设置LabelToolStripMenuItem.Click += new System.EventHandler(this.设置LabelToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // Tray
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panel1);
            resources.ApplyResources(this, "$this");
            this.Name = "Tray";
            this.Load += new System.EventHandler(this.Tray_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        protected System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 选择开始ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 选择跳过ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 全部跳过ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem 重置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置LabelToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    }
}
