using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using HZH_Controls.Controls;

namespace CommonUI
{
    public partial class RealTrayEditor : Form
    {
        private string _fileName = "";
        public RealTrayEditor(string path)
        {
            InitializeComponent();
            _fileName = path;
            LoadConfig();
        }

        private void RealTrayEditor_Load(object sender, EventArgs e)
        {

        }
        private void WriteConfigToFile()
        {
            #region 获取设置的参数
            string _ID = Textbox_WaferID.Text;
            string _Size = textBox_Size.Text;
            string _WorkZone = textBox_WorkZone.Text;
            string _CenterX = textBox_CenterX.Text;
            string _CenterY = textBox_CenterY.Text;
            int _hang = (int)MAXhang.Value;
            int _lie = (int)MaxLie.Value;
            double _X = (double)ProductX.Value;
            double _Y = (double)ProductY.Value;
            int _Mode = 0;
            if (RadioButton_hang.Checked)
            {
                _Mode = 0;
            }
            else if (RadioButton_lie.Checked)
            {
                _Mode = 1;
            }
            bool _JiaoBiao = false;
            if (Visible_jiaobiao.Checked)
            {
                _JiaoBiao = true;
            }
            else if (!Visible_jiaobiao.Checked)
            {
                _JiaoBiao = false;
            }
            bool _shizi = false;
            if (Visible_shizhi.Checked)
            {
                _shizi = true;
            }
            else if (!Visible_shizhi.Checked)
            {
                _shizi = false;
            }
            bool _bianjie = false;
            if (Visible_bianjie.Checked)
            {
                _bianjie = true;
            }
            else if (!Visible_bianjie.Checked)
            {
                _bianjie = false;
            }
            #endregion

            using (var file = new FileStream(_fileName, FileMode.Create, FileAccess.Write))
            {
                var sw = new StreamWriter(file);

                var saveObject = new
                {
                    ID = _ID,
                    WaferSetting = new
                    {
                        Size = _Size,
                        WorkZone = _WorkZone,
                        CenterX = _CenterX,
                        CenterY = _CenterY,
                        MAXhang = _hang,
                        MAXLie = _lie,
                    },
                    TestMode = _Mode,
                    ProductInfo = new
                    {
                        ProductX = _X,
                        ProductY = _Y,
                    },
                    Other = new
                    {
                        JiaoBiao = _JiaoBiao,
                        ShiZhi = _shizi,
                        BianJie = _bianjie,
                    },
                    Color = new
                    {
                        UnTest = System.Drawing.ColorTranslator.ToHtml(UnTest.BackColor),
                        Testing= System.Drawing.ColorTranslator.ToHtml(Testing.BackColor),
                        NextTest = System.Drawing.ColorTranslator.ToHtml(NextTest.BackColor),
                        SkipPos = System.Drawing.ColorTranslator.ToHtml(SkipPos.BackColor),
                        OKPos = System.Drawing.ColorTranslator.ToHtml(OKPos.BackColor),
                        NGPos = System.Drawing.ColorTranslator.ToHtml(NGPos.BackColor),
                    }
                };

                var data = JsonConvert.SerializeObject(saveObject, Formatting.Indented);
                sw.WriteLine(data);

                sw.Close();
            }
        }
        private void LoadConfig()
        {
            using (var file = new FileStream(_fileName, FileMode.Open, FileAccess.Read))
            {
                var sr = new StreamReader(file);
                JObject setting = (JObject)JsonConvert.DeserializeObject(sr.ReadToEnd());
                Textbox_WaferID.Text = setting["ID"].ToString();
                textBox_Size.Text = setting["WaferSetting"]["Size"].ToString();
                textBox_WorkZone.Text = setting["WaferSetting"]["WorkZone"].ToString();
                textBox_CenterX.Text = setting["WaferSetting"]["CenterX"].ToString();
                textBox_CenterY.Text = setting["WaferSetting"]["CenterY"].ToString();
                MAXhang.Value= (int)setting["WaferSetting"]["MAXhang"];
                MaxLie.Value = (int)setting["WaferSetting"]["MAXLie"];
                if((int)setting["TestMode"]==0)
                {
                    RadioButton_hang.Checked=true;
                    RadioButton_lie.Checked = false;
                }
                else
                {
                    RadioButton_hang.Checked = false;
                    RadioButton_lie.Checked = true;
                }
                ProductX.Value = (decimal)setting["ProductInfo"]["ProductX"];
                ProductY.Value = (decimal)setting["ProductInfo"]["ProductY"];
                if ((string)setting["Other"]["JiaoBiao"] == "True")
                {
                    Visible_jiaobiao.Checked = true;
                }
                else
                {
                    Visible_jiaobiao.Checked = false;
                }
                if ((string)setting["Other"]["ShiZhi"] == "True")
                {
                    Visible_shizhi.Checked = true;
                }
                else
                {
                    Visible_shizhi.Checked = false;
                }
                if ((string)setting["Other"]["BianJie"] == "True")
                {
                    Visible_bianjie.Checked = true;
                }
                else
                {
                    Visible_bianjie.Checked = false;
                }
                UnTest.BackColor = System.Drawing.ColorTranslator.FromHtml((string)setting["Color"]["UnTest"]);
                Testing.BackColor = System.Drawing.ColorTranslator.FromHtml((string)setting["Color"]["Testing"]);
                NextTest.BackColor = System.Drawing.ColorTranslator.FromHtml((string)setting["Color"]["NextTest"]);
                SkipPos.BackColor = System.Drawing.ColorTranslator.FromHtml((string)setting["Color"]["SkipPos"]);
                OKPos.BackColor = System.Drawing.ColorTranslator.FromHtml((string)setting["Color"]["OKPos"]);
                NGPos.BackColor = System.Drawing.ColorTranslator.FromHtml((string)setting["Color"]["NGPos"]);
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            WriteConfigToFile();
            
        }

        private void UnTest_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            UnTest.BackColor = colorDialog1.Color;
        }

        private void Testing_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            Testing.BackColor = colorDialog1.Color;
        }

        private void NextTest_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            NextTest.BackColor = colorDialog1.Color;
        }

        private void SkipPos_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            SkipPos.BackColor = colorDialog1.Color;
        }

        private void OKPos_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            OKPos.BackColor = colorDialog1.Color;
        }

        private void NGPos_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            NGPos.BackColor = colorDialog1.Color;
        }
    }
}
