using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IN3Automation.ProductSummary;

namespace IN3Automation.ProductSummary.UI
{
    public partial class ucDailySummary : UserControl
    {
        private List<DailySummary> _lastQuery;
        private ProductDataManage _dataManage;

        public ProductDataManage DataManage
        {
            get { return _dataManage; }
            set { _dataManage = value; }
        }


        public ucDailySummary()
        {
            InitializeComponent();
        }


        private async void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (_dataManage == null)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;

            var sdate = dateTimePickerStart.Value.Date;
            var edate = dateTimePickerEnd.Value.Date;

            chart1.Series["Pass"].Points.Clear();
            chart1.Series["Fail"].Points.Clear();
            chart1.Series["Meterial"].Points.Clear();
            chart1.Series["Overall Output"].Points.Clear();


            _lastQuery = await _dataManage.GetDailySummary(sdate, edate);

            foreach (var data in _lastQuery)
            {
                DateTime date = new DateTime(data.Year, data.Month, data.Day);
                chart1.Series["Pass"].Points.AddXY(date, data.Pass);
                chart1.Series["Fail"].Points.AddXY(date, data.Fail);
                chart1.Series["Meterial"].Points.AddXY(date, data.Meterial);
                chart1.Series["Overall Output"].Points.AddXY(date, data.Pass + data.Fail+data.Meterial);
            }

            chart1.Update();

            Cursor = Cursors.Arrow;
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            if (_lastQuery == null || saveFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName))
            {
                sw.WriteLine("DateTime, Pass, Fail,Meterial, Overall");

                foreach (var data in _lastQuery)
                {
                    DateTime date = new DateTime(data.Year, data.Month, data.Day);
                    sw.WriteLine($"{date:yyyy-MM-dd}, {data.Pass}, {data.Fail},{data.Meterial}, {data.Pass + data.Fail}");
                }

                sw.Close();
            }
        }
    }
}
