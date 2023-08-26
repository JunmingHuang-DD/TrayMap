namespace ProControl
{
    partial class RealTrayControl
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
            this.components = new System.ComponentModel.Container();
            this.Tray_Window = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.设置为跳过ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.重置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置为启用ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.Tray_Window)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tray_Window
            // 
            this.Tray_Window.BackColor = System.Drawing.Color.Black;
            this.Tray_Window.ContextMenuStrip = this.contextMenuStrip1;
            this.Tray_Window.Cursor = System.Windows.Forms.Cursors.Cross;
            this.Tray_Window.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tray_Window.Location = new System.Drawing.Point(0, 0);
            this.Tray_Window.Name = "Tray_Window";
            this.Tray_Window.Size = new System.Drawing.Size(550, 550);
            this.Tray_Window.TabIndex = 0;
            this.Tray_Window.TabStop = false;
            this.Tray_Window.Click += new System.EventHandler(this.Tray_Window_Click);
            this.Tray_Window.Paint += new System.Windows.Forms.PaintEventHandler(this.Tray_Window_Paint);
            this.Tray_Window.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Tray_Window_MouseDown);
            this.Tray_Window.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Tray_Window_MouseMove);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripSeparator3,
            this.设置为跳过ToolStripMenuItem,
            this.toolStripSeparator1,
            this.设置为启用ToolStripMenuItem,
            this.toolStripSeparator2,
            this.重置ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 132);
            // 
            // 设置为跳过ToolStripMenuItem
            // 
            this.设置为跳过ToolStripMenuItem.Name = "设置为跳过ToolStripMenuItem";
            this.设置为跳过ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.设置为跳过ToolStripMenuItem.Text = "设置为跳过";
            this.设置为跳过ToolStripMenuItem.Click += new System.EventHandler(this.设置为跳过ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // 重置ToolStripMenuItem
            // 
            this.重置ToolStripMenuItem.Name = "重置ToolStripMenuItem";
            this.重置ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.重置ToolStripMenuItem.Text = "重置";
            this.重置ToolStripMenuItem.Click += new System.EventHandler(this.重置ToolStripMenuItem_Click);
            // 
            // 设置为启用ToolStripMenuItem
            // 
            this.设置为启用ToolStripMenuItem.Name = "设置为启用ToolStripMenuItem";
            this.设置为启用ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.设置为启用ToolStripMenuItem.Text = "设置为启用";
            this.设置为启用ToolStripMenuItem.Click += new System.EventHandler(this.设置为启用ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem1.Text = "设置为下一个";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
            // 
            // RealTrayControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Tray_Window);
            this.Name = "RealTrayControl";
            this.Size = new System.Drawing.Size(550, 550);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RealTrayControl_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RealTrayControl_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.Tray_Window)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Tray_Window;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 设置为跳过ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 重置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置为启用ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}
