using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonUI
{
    public partial class TeachItemControl : UserControl
    {
        public string LabelName 
        {
            get { return labelName.Text; }
            set { labelName.Text = value; }
        }

        public double TextValue
        {
            get { return double.Parse(tbValue.Text); }
            set
            {
                if (labelName.Text == "Pick_R")
                {
                    label4.Text = "度";
                    label11.Text = "度/s";
                    label5.Text = "度/s";
                    label6.Text = "度/s2";
                    label7.Text = "度/s2";
                }
                tbValue.Text = value.ToString("F3");
            }
        }
            
        public double StartSpeed
            {
            get { return double.Parse(tbStartSpeed.Text); }
            set { tbStartSpeed.Text = value.ToString(); }
            }
        public double Speed
        {
            get { return double.Parse(tbSpeed.Text); }
            set
            {
                tbSpeed.Text = value.ToString();
            }
        }

        public double Acc
        {
            get { return double.Parse(tbAcc.Text); }
            set
            {
                tbAcc.Text = value.ToString();
            }
        }

        public double Dec
        {
            get { return double.Parse(tbDec.Text); }
            set
            {
                tbDec.Text = value.ToString();
            }
        }

        public double SmoothTime
            {
            get { return double.Parse(tbSmoothTime.Text); }
            set { tbSmoothTime.Text = value.ToString(); }
            }

        public bool ReadOnly
        {
            get { return tbValue.ReadOnly; }
            set
            {
                tbValue.ReadOnly = value;
            }
        }

        public string Key { get; set; }

        public TeachItemControl()
        {
            InitializeComponent();

            tbValue.Text = "0";
            StartSpeed = 0;
            Speed = 0;
            Acc = 0;
            Dec = 0;
            SmoothTime = 0;
           
        }

    }
}
