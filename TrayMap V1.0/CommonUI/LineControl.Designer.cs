namespace CommonUI
{
    partial class LineControl
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblLineName = new System.Windows.Forms.Label();
            this.btnState = new CommonUI.Buttons.RoundButton();
            this.SuspendLayout();
            // 
            // lblLineName
            // 
            this.lblLineName.AutoSize = true;
            this.lblLineName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLineName.ForeColor = System.Drawing.Color.Black;
            this.lblLineName.Location = new System.Drawing.Point(3, 3);
            this.lblLineName.Name = "lblLineName";
            this.lblLineName.Size = new System.Drawing.Size(67, 13);
            this.lblLineName.TabIndex = 0;
            this.lblLineName.Text = "Line Name";
            // 
            // btnState
            // 
            this.btnState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnState.BackColor = System.Drawing.Color.Lime;
            this.btnState.Enabled = false;
            this.btnState.Location = new System.Drawing.Point(166, 2);
            this.btnState.Name = "btnState";
            this.btnState.RecessDepth = 1;
            this.btnState.Size = new System.Drawing.Size(32, 22);
            this.btnState.TabIndex = 1;
            this.btnState.Text = null;
            this.btnState.UseVisualStyleBackColor = false;
            this.btnState.Click += new System.EventHandler(this.btnState_Click);
            // 
            // LineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnState);
            this.Controls.Add(this.lblLineName);
            this.Name = "LineControl";
            this.Size = new System.Drawing.Size(201, 27);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLineName;
        private Buttons.RoundButton btnState;
    }
}
