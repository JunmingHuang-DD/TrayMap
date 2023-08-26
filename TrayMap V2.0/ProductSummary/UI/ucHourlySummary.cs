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
    public partial class ucHourlySummary : UserControl
    {
        
        private List<HourlySummary> _lastQuery;
        private ProductDataManage _dataManage;

        public ProductDataManage DataManage
        {
            get { return _dataManage; }
            set { _dataManage = value; }
        }

        public ucHourlySummary()
        {
            InitializeComponent();
        }


        public ucHourlySummary(ProductDataManage data) : this()
        {
            _dataManage = data;
        }


        private async void buttonUpdate_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            var date = dateTimePicker1.Value.Date;

            chart1.Series["Pass"].Points.Clear();
            chart1.Series["Fail"].Points.Clear();
            chart1.Series["Meterial"].Points.Clear();
            chart1.Series["Overall Output"].Points.Clear();


            _lastQuery = await _dataManage.GetHourlySummary(date);

            chart1.ChartAreas[0].AxisX.Title = $"Date {date:d} / by hour";

            foreach (var data in _lastQuery)
            {
                chart1.Series["Pass"].Points.AddXY(data.Hour, data.Pass);
                chart1.Series["Fail"].Points.AddXY(data.Hour, data.Fail);
                chart1.Series["Meterial"].Points.AddXY(data.Hour, data.Meterial);
                chart1.Series["Overall Output"].Points.AddXY(data.Hour, data.Pass + data.Fail+data.Meterial);
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
                sw.WriteLine("DateTime, Hour, Pass, Fail,Meterial, Overall");
                foreach (var data in _lastQuery)
                {
                    sw.WriteLine($"{data.Day:yyyy-MM-dd}, {data.Hour}, {data.Pass}, {data.Fail}, {data.Meterial},{data.Pass + data.Fail}");
                }

                sw.Close();
            }
        }
    }
}
