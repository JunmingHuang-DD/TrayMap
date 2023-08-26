// ===============================================================================
// Project Name        :    CommonUI
// Project Description :    
// ===============================================================================
// Class Name          :    DigitTextBox
// Class Version       :    v1.0.0.0
// Class Description   :    
// Author              :    Administrator
// Create Time         :    2014/10/11 13:57:49
// Update Time         :    2014/10/11 13:57:49
// ===============================================================================
// Copyright © IN3 2014 . All rights reserved.
// ===============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace CommonUI
{
    public class DigitTextBox : TextBox
    {
        public DigitTextBox()
        {
            this.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
            this.TextAlign = HorizontalAlignment.Center;
            this.Text = "0";
        }

        [Browsable(false)]
        public double Value
        {
            get
            {
                return double.Parse(this.Text);
            }
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) &&
                !Char.IsControl(e.KeyChar) &&
                e.KeyChar != '.' &&
                e.KeyChar != '-')
            {
                e.Handled = true;
            }

            //prevent double point
            if (this.Text.Contains('.') && e.KeyChar == '.')
            {
                e.Handled = true;
            }

            //prevent input - in middle
            if (this.Text.Length > 0 && e.KeyChar == '-')
            {
                e.Handled = true;
            }

            base.OnKeyPress(e);
        }
    }
}
