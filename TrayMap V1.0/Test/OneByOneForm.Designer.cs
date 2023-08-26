namespace Test
{
    partial class OneByOneForm
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
            this.trayOneByOne = new CommonUI.Tray();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnSetArray = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // trayOneByOne
            // 
            this.trayOneByOne.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.trayOneByOne.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.trayOneByOne.CurrentOperation = ((CommonUI.Tray.OperationType)(((CommonUI.Tray.OperationType.Skip | CommonUI.Tray.OperationType.PickCandinate) 
            | CommonUI.Tray.OperationType.NoneDefine)));
            this.trayOneByOne.IsAutoUpdata = false;
            this.trayOneByOne.IsShowCharacter = false;
            this.trayOneByOne.IsShowName = false;
            this.trayOneByOne.Location = new System.Drawing.Point(13, 13);
            this.trayOneByOne.Margin = new System.Windows.Forms.Padding(4);
            this.trayOneByOne.Name = "trayOneByOne";
            this.trayOneByOne.Size = new System.Drawing.Size(466, 488);
            this.trayOneByOne.TabIndex = 0;
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(520, 52);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(87, 34);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "下一个";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnSetArray
            // 
            this.btnSetArray.Location = new System.Drawing.Point(520, 232);
            this.btnSetArray.Name = "btnSetArray";
            this.btnSetArray.Size = new System.Drawing.Size(87, 34);
            this.btnSetArray.TabIndex = 2;
            this.btnSetArray.Text = "设置队列";
            this.btnSetArray.UseVisualStyleBackColor = true;
            this.btnSetArray.Click += new System.EventHandler(this.btnSetArray_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(532, 349);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // OneByOneForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 598);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSetArray);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.trayOneByOne);
            this.Name = "OneByOneForm";
            this.Text = "OneByOneForm";
            this.ResumeLayout(false);

        }

        #endregion

        private CommonUI.Tray trayOneByOne;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnSetArray;
        private System.Windows.Forms.Button button1;
    }
}