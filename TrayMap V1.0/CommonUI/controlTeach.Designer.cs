namespace CommonUI
{
    partial class controlTeach
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(controlTeach));
            this.textBoxDes = new System.Windows.Forms.TextBox();
            this.flpContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.gridTeachData = new CommonUI.OutlookGrid.OutlookGrid();
            this.ColumnCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column70 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonUpdateSpeed = new CommonUI.Buttons.GlassButton();
            this.buttonUpdate = new CommonUI.Buttons.GlassButton();
            this.buttonSaveUpdate = new CommonUI.Buttons.RibbonMenuButton();
            this.glassButtonMove = new CommonUI.Buttons.GlassButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ProgramName = new System.Windows.Forms.TextBox();
            this.ProgramSaveAs = new CommonUI.Buttons.RibbonMenuButton();
            this.ProgramLoad = new CommonUI.Buttons.RibbonMenuButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridTeachData)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxDes
            // 
            this.textBoxDes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDes.BackColor = System.Drawing.Color.White;
            this.textBoxDes.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxDes.ForeColor = System.Drawing.SystemColors.ControlText;
            this.textBoxDes.Location = new System.Drawing.Point(249, 575);
            this.textBoxDes.Multiline = true;
            this.textBoxDes.Name = "textBoxDes";
            this.textBoxDes.ReadOnly = true;
            this.textBoxDes.Size = new System.Drawing.Size(516, 50);
            this.textBoxDes.TabIndex = 1;
            this.textBoxDes.Text = "this is some sample";
            // 
            // flpContainer
            // 
            this.flpContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.flpContainer.AutoScroll = true;
            this.flpContainer.AutoScrollMinSize = new System.Drawing.Size(200, 380);
            this.flpContainer.Location = new System.Drawing.Point(3, 222);
            this.flpContainer.Name = "flpContainer";
            this.flpContainer.Size = new System.Drawing.Size(240, 403);
            this.flpContainer.TabIndex = 5;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelTitle.Location = new System.Drawing.Point(3, 203);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(80, 16);
            this.labelTitle.TabIndex = 6;
            this.labelTitle.Text = "Selected";
            // 
            // gridTeachData
            // 
            this.gridTeachData.AllowUserToAddRows = false;
            this.gridTeachData.AllowUserToDeleteRows = false;
            this.gridTeachData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridTeachData.CollapseIcon = null;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridTeachData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridTeachData.ColumnHeadersHeight = 28;
            this.gridTeachData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnCategory,
            this.column70,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.ColumnName});
            this.gridTeachData.ExpandIcon = null;
            this.gridTeachData.Location = new System.Drawing.Point(249, 50);
            this.gridTeachData.MultiSelect = false;
            this.gridTeachData.Name = "gridTeachData";
            this.gridTeachData.ReadOnly = true;
            this.gridTeachData.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridTeachData.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gridTeachData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridTeachData.Size = new System.Drawing.Size(516, 519);
            this.gridTeachData.TabIndex = 10;
            this.gridTeachData.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellEnter);
            // 
            // ColumnCategory
            // 
            this.ColumnCategory.HeaderText = "分组信息";
            this.ColumnCategory.Name = "ColumnCategory";
            this.ColumnCategory.ReadOnly = true;
            this.ColumnCategory.Visible = false;
            // 
            // column70
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.column70.DefaultCellStyle = dataGridViewCellStyle2;
            this.column70.HeaderText = "参数名称";
            this.column70.Name = "column70";
            this.column70.ReadOnly = true;
            this.column70.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column7
            // 
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Column7.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column7.HeaderText = "参数内容";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "位置点数据";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "更新时间";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "说明";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column10.Visible = false;
            // 
            // ColumnName
            // 
            this.ColumnName.HeaderText = "名称";
            this.ColumnName.Name = "ColumnName";
            this.ColumnName.ReadOnly = true;
            this.ColumnName.Visible = false;
            // 
            // buttonUpdateSpeed
            // 
            this.buttonUpdateSpeed.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonUpdateSpeed.BackgroundImage = global::CommonUI.Properties.Resources.software_update_300x300;
            this.buttonUpdateSpeed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonUpdateSpeed.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.buttonUpdateSpeed.ForeColor = System.Drawing.Color.Purple;
            this.buttonUpdateSpeed.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonUpdateSpeed.InnerBorderColor = System.Drawing.Color.DimGray;
            this.buttonUpdateSpeed.Location = new System.Drawing.Point(25, 111);
            this.buttonUpdateSpeed.Name = "buttonUpdateSpeed";
            this.buttonUpdateSpeed.Size = new System.Drawing.Size(175, 40);
            this.buttonUpdateSpeed.TabIndex = 7;
            this.buttonUpdateSpeed.Text = "更新速度";
            this.buttonUpdateSpeed.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonUpdateSpeed.Click += new System.EventHandler(this.buttonUpdateSpeed_Click);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonUpdate.BackgroundImage = global::CommonUI.Properties.Resources.software_update_300x300;
            this.buttonUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonUpdate.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.buttonUpdate.ForeColor = System.Drawing.Color.Maroon;
            this.buttonUpdate.InnerBorderColor = System.Drawing.Color.DimGray;
            this.buttonUpdate.Location = new System.Drawing.Point(25, 160);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(175, 40);
            this.buttonUpdate.TabIndex = 7;
            this.buttonUpdate.Text = "更新速度、位置";
            this.buttonUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // buttonSaveUpdate
            // 
            this.buttonSaveUpdate.Arrow = CommonUI.Buttons.RibbonMenuButton.e_arrow.None;
            this.buttonSaveUpdate.BackColor = System.Drawing.Color.Transparent;
            this.buttonSaveUpdate.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.buttonSaveUpdate.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.buttonSaveUpdate.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(78)))));
            this.buttonSaveUpdate.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(177)))), ((int)(((byte)(118)))));
            this.buttonSaveUpdate.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonSaveUpdate.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonSaveUpdate.FadingSpeed = 35;
            this.buttonSaveUpdate.FlatAppearance.BorderSize = 0;
            this.buttonSaveUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.buttonSaveUpdate.GroupPos = CommonUI.Buttons.RibbonMenuButton.e_groupPos.None;
            this.buttonSaveUpdate.Image = ((System.Drawing.Image)(resources.GetObject("buttonSaveUpdate.Image")));
            this.buttonSaveUpdate.ImageLocation = CommonUI.Buttons.RibbonMenuButton.e_imagelocation.Left;
            this.buttonSaveUpdate.ImageOffset = 0;
            this.buttonSaveUpdate.IsPressed = false;
            this.buttonSaveUpdate.KeepPress = false;
            this.buttonSaveUpdate.Location = new System.Drawing.Point(25, 61);
            this.buttonSaveUpdate.MaxImageSize = new System.Drawing.Point(0, 0);
            this.buttonSaveUpdate.MenuPos = new System.Drawing.Point(0, 0);
            this.buttonSaveUpdate.Name = "buttonSaveUpdate";
            this.buttonSaveUpdate.Radius = 6;
            this.buttonSaveUpdate.ShowBase = CommonUI.Buttons.RibbonMenuButton.e_showbase.Yes;
            this.buttonSaveUpdate.Size = new System.Drawing.Size(175, 37);
            this.buttonSaveUpdate.SplitButton = CommonUI.Buttons.RibbonMenuButton.e_splitbutton.No;
            this.buttonSaveUpdate.SplitDistance = 0;
            this.buttonSaveUpdate.TabIndex = 9;
            this.buttonSaveUpdate.Text = "   保存更新";
            this.buttonSaveUpdate.Title = "";
            this.buttonSaveUpdate.UseVisualStyleBackColor = false;
            this.buttonSaveUpdate.Click += new System.EventHandler(this.buttonSaveUpdate_Click);
            // 
            // glassButtonMove
            // 
            this.glassButtonMove.BackColor = System.Drawing.Color.Orange;
            this.glassButtonMove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.glassButtonMove.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.glassButtonMove.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.glassButtonMove.InnerBorderColor = System.Drawing.Color.DimGray;
            this.glassButtonMove.Location = new System.Drawing.Point(25, 4);
            this.glassButtonMove.Name = "glassButtonMove";
            this.glassButtonMove.Size = new System.Drawing.Size(175, 40);
            this.glassButtonMove.TabIndex = 7;
            this.glassButtonMove.Text = "移动到示教点";
            this.glassButtonMove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.glassButtonMove.Click += new System.EventHandler(this.glassButtonMove_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.textBox1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox1.Location = new System.Drawing.Point(396, 9);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ShortcutsEnabled = false;
            this.textBox1.Size = new System.Drawing.Size(36, 35);
            this.textBox1.TabIndex = 15;
            this.textBox1.Text = "当前\r\n程序";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ProgramName
            // 
            this.ProgramName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.ProgramName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ProgramName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ProgramName.Location = new System.Drawing.Point(434, 9);
            this.ProgramName.Multiline = true;
            this.ProgramName.Name = "ProgramName";
            this.ProgramName.ReadOnly = true;
            this.ProgramName.ShortcutsEnabled = false;
            this.ProgramName.Size = new System.Drawing.Size(184, 35);
            this.ProgramName.TabIndex = 16;
            // 
            // ProgramSaveAs
            // 
            this.ProgramSaveAs.Arrow = CommonUI.Buttons.RibbonMenuButton.e_arrow.None;
            this.ProgramSaveAs.BackColor = System.Drawing.Color.Transparent;
            this.ProgramSaveAs.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.ProgramSaveAs.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.ProgramSaveAs.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(78)))));
            this.ProgramSaveAs.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(177)))), ((int)(((byte)(118)))));
            this.ProgramSaveAs.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.ProgramSaveAs.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.ProgramSaveAs.FadingSpeed = 35;
            this.ProgramSaveAs.FlatAppearance.BorderSize = 0;
            this.ProgramSaveAs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ProgramSaveAs.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.ProgramSaveAs.GroupPos = CommonUI.Buttons.RibbonMenuButton.e_groupPos.None;
            this.ProgramSaveAs.Image = null;
            this.ProgramSaveAs.ImageLocation = CommonUI.Buttons.RibbonMenuButton.e_imagelocation.Left;
            this.ProgramSaveAs.ImageOffset = 0;
            this.ProgramSaveAs.IsPressed = false;
            this.ProgramSaveAs.KeepPress = false;
            this.ProgramSaveAs.Location = new System.Drawing.Point(624, 9);
            this.ProgramSaveAs.MaxImageSize = new System.Drawing.Point(0, 0);
            this.ProgramSaveAs.MenuPos = new System.Drawing.Point(0, 0);
            this.ProgramSaveAs.Name = "ProgramSaveAs";
            this.ProgramSaveAs.Radius = 6;
            this.ProgramSaveAs.ShowBase = CommonUI.Buttons.RibbonMenuButton.e_showbase.Yes;
            this.ProgramSaveAs.Size = new System.Drawing.Size(141, 37);
            this.ProgramSaveAs.SplitButton = CommonUI.Buttons.RibbonMenuButton.e_splitbutton.No;
            this.ProgramSaveAs.SplitDistance = 0;
            this.ProgramSaveAs.TabIndex = 11;
            this.ProgramSaveAs.Text = "  另存为...";
            this.ProgramSaveAs.Title = "";
            this.ProgramSaveAs.UseVisualStyleBackColor = false;
            this.ProgramSaveAs.Click += new System.EventHandler(this.ProgramSaveAs_Click);
            // 
            // ProgramLoad
            // 
            this.ProgramLoad.Arrow = CommonUI.Buttons.RibbonMenuButton.e_arrow.None;
            this.ProgramLoad.BackColor = System.Drawing.Color.Transparent;
            this.ProgramLoad.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.ProgramLoad.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(187)))), ((int)(((byte)(213)))));
            this.ProgramLoad.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(78)))));
            this.ProgramLoad.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(177)))), ((int)(((byte)(118)))));
            this.ProgramLoad.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.ProgramLoad.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.ProgramLoad.FadingSpeed = 35;
            this.ProgramLoad.FlatAppearance.BorderSize = 0;
            this.ProgramLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ProgramLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.ProgramLoad.GroupPos = CommonUI.Buttons.RibbonMenuButton.e_groupPos.None;
            this.ProgramLoad.Image = null;
            this.ProgramLoad.ImageLocation = CommonUI.Buttons.RibbonMenuButton.e_imagelocation.Left;
            this.ProgramLoad.ImageOffset = 0;
            this.ProgramLoad.IsPressed = false;
            this.ProgramLoad.KeepPress = false;
            this.ProgramLoad.Location = new System.Drawing.Point(249, 7);
            this.ProgramLoad.MaxImageSize = new System.Drawing.Point(0, 0);
            this.ProgramLoad.MenuPos = new System.Drawing.Point(0, 0);
            this.ProgramLoad.Name = "ProgramLoad";
            this.ProgramLoad.Radius = 6;
            this.ProgramLoad.ShowBase = CommonUI.Buttons.RibbonMenuButton.e_showbase.Yes;
            this.ProgramLoad.Size = new System.Drawing.Size(141, 37);
            this.ProgramLoad.SplitButton = CommonUI.Buttons.RibbonMenuButton.e_splitbutton.No;
            this.ProgramLoad.SplitDistance = 0;
            this.ProgramLoad.TabIndex = 12;
            this.ProgramLoad.Text = "  程序选择...";
            this.ProgramLoad.Title = "";
            this.ProgramLoad.UseVisualStyleBackColor = false;
            this.ProgramLoad.Click += new System.EventHandler(this.ProgramLoad_Click);
            // 
            // controlTeach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.ProgramName);
            this.Controls.Add(this.ProgramSaveAs);
            this.Controls.Add(this.ProgramLoad);
            this.Controls.Add(this.gridTeachData);
            this.Controls.Add(this.buttonUpdateSpeed);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.buttonSaveUpdate);
            this.Controls.Add(this.glassButtonMove);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.flpContainer);
            this.Controls.Add(this.textBoxDes);
            this.Name = "controlTeach";
            this.Size = new System.Drawing.Size(768, 628);
            this.Load += new System.EventHandler(this.controlTeach_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridTeachData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxDes;
        private System.Windows.Forms.FlowLayoutPanel flpContainer;
        private System.Windows.Forms.Label labelTitle;
        private Buttons.GlassButton buttonUpdate;
        private Buttons.GlassButton glassButtonMove;
        private Buttons.GlassButton buttonUpdateSpeed;
        private Buttons.RibbonMenuButton buttonSaveUpdate;
        private OutlookGrid.OutlookGrid gridTeachData;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn column70;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox ProgramName;
        private Buttons.RibbonMenuButton ProgramSaveAs;
        private Buttons.RibbonMenuButton ProgramLoad;
    }
}
