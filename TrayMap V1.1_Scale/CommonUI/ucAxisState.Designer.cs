namespace CommonUI
{
    partial class ucAxisState
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
            if (_Axis != null)
            {
                _Axis.PositionUpdate -= new System.EventHandler(_Axis_PositionUpdate);
            }

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
            this.tbPosition = new System.Windows.Forms.TextBox();
            this.rbAxisName = new CommonUI.Buttons.GlassButton();
            this.SuspendLayout();
            // 
            // tbPosition
            // 
            this.tbPosition.BackColor = System.Drawing.Color.Black;
            this.tbPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPosition.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbPosition.Location = new System.Drawing.Point(155, -1);
            this.tbPosition.Multiline = true;
            this.tbPosition.Name = "tbPosition";
            this.tbPosition.ReadOnly = true;
            this.tbPosition.Size = new System.Drawing.Size(110, 31);
            this.tbPosition.TabIndex = 14;
            this.tbPosition.Tag = "Tray_Y2";
            this.tbPosition.Text = "0";
            this.tbPosition.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // rbAxisName
            // 
            this.rbAxisName.AlternativeFocusBorderColor = System.Drawing.Color.Red;
            this.rbAxisName.BackColor = System.Drawing.Color.Lime;
            this.rbAxisName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbAxisName.ForeColor = System.Drawing.Color.Black;
            this.rbAxisName.InnerBorderColor = System.Drawing.SystemColors.Window;
            this.rbAxisName.Location = new System.Drawing.Point(0, 0);
            this.rbAxisName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbAxisName.Name = "rbAxisName";
            this.rbAxisName.OuterBorderColor = System.Drawing.Color.Red;
            this.rbAxisName.Size = new System.Drawing.Size(149, 29);
            this.rbAxisName.TabIndex = 26;
            this.rbAxisName.Text = "X";
            this.rbAxisName.Click += new System.EventHandler(this.rbAxisName_Click);
            // 
            // ucAxisState
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rbAxisName);
            this.Controls.Add(this.tbPosition);
            this.Name = "ucAxisState";
            this.Size = new System.Drawing.Size(263, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbPosition;
        private Buttons.GlassButton rbAxisName;
    }
}
