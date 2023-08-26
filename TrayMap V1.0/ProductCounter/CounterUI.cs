using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IN3Automation.ProductCounter;

namespace IN3Automation.ProductCounter
{
    public partial class CounterUI : UserControl
    {
        private ProductionCount _ProductCount;

        public ProductionCount ProductCount
        {
            set { _ProductCount = value;
                     if(_ProductCount != null)
                    {
                        _ProductCount.CountChanged += OnCountChanged;
                    }
                 }
            get { return _ProductCount; }
        }

        public CounterUI()
        {
            InitializeComponent();           
        }

        public void OnCountChanged(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() =>
            {
                textBoxNG.Text = _ProductCount.BadCount.ToString();
                textBoxTotalOutput.Text = (_ProductCount.TotalCount).ToString();
                textBoxYield.Text = _ProductCount.Yield.ToString("F2");
                textBoxUPH.Text = _ProductCount.AveragedUPH.ToString("F1");
            }));
        }

        private void btnEmpty_Click(object sender, EventArgs e)
        {
            if(DialogResult.OK == MessageBox.Show("是否确定清空总产量??","清空产量",MessageBoxButtons.OKCancel))
            {
                _ProductCount.Clear();
            }          
        }
    }
}
