using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Motion
{
    public partial class SettingsEditor : Form
    {
        public SettingsEditor()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //生成Excel模板
            //create a xlsx file 
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            FileInfo file = new FileInfo(saveFileDialog1.FileName);
            if (file.Exists)
            {
                File.Delete(saveFileDialog1.FileName);
            }

            ExcelPackage ep = CreateWorkSheet();

            ep.File = file;

            ep.Save();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            //加载配置文件生成Excel
            //load current Setting File
            if (openFileDialog2.ShowDialog() != DialogResult.OK)
            {
                return;
            }


            var ep = CreateWorkSheet();
            ReadSeting(openFileDialog2.FileName, ep);

            MessageBox.Show("选择要保存的Excel文件路径");

            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            FileInfo file = new FileInfo(saveFileDialog1.FileName);
            if (file.Exists)
            {
                File.Delete(saveFileDialog1.FileName);
            }

            ep.File = file;
            ep.Save();

            MessageBox.Show("现在可以去打开Excel文件编辑了");

            System.Diagnostics.Process.Start(saveFileDialog1.FileName);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //加载Excel生成配置文件
            //Parse an Excel file
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            ExcelPackage ep = new ExcelPackage(new FileInfo(openFileDialog1.FileName));

            MessageBox.Show("选择要保存的轴配置文件路径");

            if (saveFileDialog2.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            SaveSetting(ep, saveFileDialog2.FileName);
        }

        #region Excel manipulate
        /// <summary>
        /// 创建Excel模板文件
        /// </summary>
        /// <returns></returns>
        private ExcelPackage CreateWorkSheet()
        {
            ExcelPackage ep = new ExcelPackage();//file

            //add axes page
            var workSheet = ep.Workbook.Worksheets.Add("Axes");

            //add headers
            workSheet.Cells[1, 1].Value = "Axis_Name";
            workSheet.Cells[1, 2].Value = "Axis_Display";
            workSheet.Cells[1, 3].Value = "Axis_CardType";
            workSheet.Cells[1, 4].Value = "Axis_CardID";
            workSheet.Cells[1, 5].Value = "Axis_Index";
            workSheet.Cells[1, 6].Value = "Axis_Positive";
            workSheet.Cells[1, 7].Value = "Axis_Negative";
            workSheet.Cells[1, 8].Value = "Axis_CountPerMM";
            workSheet.Cells[1, 9].Value = "Axis_Direct";
            workSheet.Cells[1, 10].Value = "Axis_UseEncoder";
            workSheet.Cells[1, 11].Value = "Home_Mode";
            workSheet.Cells[1, 12].Value = "Home_Direction";
            workSheet.Cells[1, 13].Value = "Home_Offset";
            workSheet.Cells[1, 14].Value = "Home_HomePosition";
            workSheet.Cells[1, 15].Value = "Home_StartSpeed";
            workSheet.Cells[1, 16].Value = "Home_Speed";
            workSheet.Cells[1, 17].Value = "Home_Acc";
            workSheet.Cells[1, 18].Value = "Home_LeaveHomeSpeed";
            workSheet.Cells[1, 1, 1, 18].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            workSheet.Cells[1, 1, 1, 10].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
            workSheet.Cells[1, 11, 1, 17].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
            workSheet.Cells[1, 1, 1, 18].Style.Font.Bold = true;
            workSheet.Cells[1, 1, 1, 18].AutoFitColumns();
            workSheet.Cells[1, 1, 1, 18].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);


            //add data validator
            string[] boolValidator = new string[] { "True", "False" };
            var validRange = workSheet.DataValidations.AddListValidation(workSheet.Cells[2, 10, 122, 10].Address);
            validRange.ShowInputMessage = true;
            for (int i = 0; i < boolValidator.Length; i++)
            {
                validRange.Formula.Values.Add(boolValidator[i]);
            }



            //add Input IO
            var inputSheet = ep.Workbook.Worksheets.Add("Inputs");
            //add headers
            inputSheet.Cells[1, 1].Value = "Name";
            inputSheet.Cells[1, 2].Value = "Display";
            inputSheet.Cells[1, 3].Value = "Card Type";
            inputSheet.Cells[1, 4].Value = "Card ID";
            inputSheet.Cells[1, 5].Value = "Index";
            inputSheet.Cells[1, 6].Value = "Port";
            inputSheet.Cells[1, 7].Value = "Reversed";
            inputSheet.Cells[1, 1, 1, 7].Style.Font.Bold = true;
            inputSheet.Cells[1, 1, 1, 7].AutoFitColumns();
            inputSheet.Cells[1, 1, 1, 7].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            inputSheet.Cells[1, 1, 1, 7].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
            inputSheet.Cells[1, 1, 1, 7].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

            validRange = inputSheet.DataValidations.AddListValidation(inputSheet.Cells[2, 7, 100, 7].Address);
            validRange.ShowInputMessage = true;
            for (int i = 0; i < boolValidator.Length; i++)
            {
                validRange.Formula.Values.Add(boolValidator[i]);
            }

            //add Output IO
            var outputSheet = ep.Workbook.Worksheets.Add("Outputs");
            //add headers
            outputSheet.Cells[1, 1].Value = "Name";
            outputSheet.Cells[1, 2].Value = "Display";
            outputSheet.Cells[1, 3].Value = "Card Type";
            outputSheet.Cells[1, 4].Value = "Card ID";
            outputSheet.Cells[1, 5].Value = "Index";
            outputSheet.Cells[1, 6].Value = "Port";
            outputSheet.Cells[1, 7].Value = "Reversed";
            outputSheet.Cells[1, 1, 1, 7].Style.Font.Bold = true;
            outputSheet.Cells[1, 1, 1, 7].AutoFitColumns();
            outputSheet.Cells[1, 1, 1, 7].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            outputSheet.Cells[1, 1, 1, 7].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
            outputSheet.Cells[1, 1, 1, 7].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

            validRange = outputSheet.DataValidations.AddListValidation(outputSheet.Cells[2, 7, 100, 7].Address);
            validRange.ShowInputMessage = true;
            for (int i = 0; i < boolValidator.Length; i++)
            {
                validRange.Formula.Values.Add(boolValidator[i]);
            }

            return ep;
        }

        /// <summary>
        /// 从配置文件读取到Excel中
        /// </summary>
        /// <param name="file"></param>
        /// <param name="ep"></param>
        private void ReadSeting(string file, ExcelPackage ep)
        {
            XDocument doc = XDocument.Load(file);

            int row = 2;

            var workSheet = ep.Workbook.Worksheets["Axes"];
            foreach (var axis in doc.Descendants("Axis"))
            {
                workSheet.Cells[row, 1].Value = axis.Attribute("Name").Value;
                workSheet.Cells[row, 2].Value = axis.Attribute("Display").Value;
                workSheet.Cells[row, 3].Value = axis.Attribute("CardType").Value;
                workSheet.Cells[row, 4].Value = double.Parse(axis.Attribute("CardID").Value);
                workSheet.Cells[row, 5].Value = double.Parse(axis.Attribute("Index").Value);
                workSheet.Cells[row, 6].Value = double.Parse(axis.Attribute("Positive").Value);
                workSheet.Cells[row, 7].Value = double.Parse(axis.Attribute("Negative").Value);
                workSheet.Cells[row, 8].Value = double.Parse(axis.Attribute("CountPerMM").Value);
                workSheet.Cells[row, 9].Value = double.Parse(axis.Attribute("Direct").Value);
                workSheet.Cells[row, 10].Value = axis.Attribute("UseEncoder").Value;


                XElement home = axis.Element("homeParam");
                workSheet.Cells[row, 11].Value = double.Parse(home.Attribute("Mode").Value);
                workSheet.Cells[row, 12].Value = double.Parse(home.Attribute("Direction").Value);
                workSheet.Cells[row, 13].Value = double.Parse(home.Attribute("Offset").Value);
                workSheet.Cells[row, 14].Value = double.Parse(home.Attribute("HomePosition").Value);
                workSheet.Cells[row, 15].Value = double.Parse(home.Attribute("StartSpeed").Value);
                workSheet.Cells[row, 16].Value = double.Parse(home.Attribute("Speed").Value);
                workSheet.Cells[row, 17].Value = double.Parse(home.Attribute("Acc").Value);
                workSheet.Cells[row, 18].Value = double.Parse(home.Attribute("LeaveHomeSpeed").Value);

                row++;
            }
            workSheet.Cells.AutoFitColumns();


            row = 2;
            var inputSheet = ep.Workbook.Worksheets["Inputs"];
            foreach (var line in doc.Descendants("Input"))
            {
                inputSheet.Cells[row, 1].Value = line.Attribute("name").Value;
                inputSheet.Cells[row, 2].Value = line.Attribute("display").Value;
                inputSheet.Cells[row, 3].Value = line.Attribute("cardtype").Value;
                inputSheet.Cells[row, 4].Value = double.Parse(line.Attribute("cardid").Value);
                inputSheet.Cells[row, 5].Value = double.Parse(line.Attribute("index").Value);
                inputSheet.Cells[row, 6].Value = double.Parse(line.Attribute("port").Value);
                inputSheet.Cells[row, 7].Value = line.Attribute("reversed").Value;

                row++;
            }
            inputSheet.Cells.AutoFitColumns();

            row = 2;
            var outputSheet = ep.Workbook.Worksheets["Outputs"];
            foreach (var line in doc.Descendants("Output"))
            {
                outputSheet.Cells[row, 1].Value = line.Attribute("name").Value;
                outputSheet.Cells[row, 2].Value = line.Attribute("display").Value;
                outputSheet.Cells[row, 3].Value = line.Attribute("cardtype").Value;
                outputSheet.Cells[row, 4].Value = double.Parse(line.Attribute("cardid").Value);
                outputSheet.Cells[row, 5].Value = double.Parse(line.Attribute("index").Value);
                outputSheet.Cells[row, 6].Value = double.Parse(line.Attribute("port").Value);
                outputSheet.Cells[row, 7].Value = line.Attribute("reversed").Value;

                row++;
            }
            outputSheet.Cells.AutoFitColumns();
        }

        /// <summary>
        /// 保存到配置文件中
        /// </summary>
        /// <param name="ep"></param>
        /// <param name="fileName"></param>
        private void SaveSetting(ExcelPackage ep, string fileName = "Motion.xml")
        {
            XDocument doc = new XDocument();

            XElement root = new XElement("MotionSetting");
            doc.Add(root);

            XElement axes = new XElement("MotionAxes");
            root.Add(axes);

            var workSheet = ep.Workbook.Worksheets["Axes"];

            for (int i = 0; i < 50; i++) //workSheet.Cells.Rows
            {
                if (workSheet.Cells[i + 2, 1].Value == null ||
                    workSheet.Cells[i + 2, 1].Value.ToString().Length < 1)
                {
                    continue;
                }

                XElement axis = new XElement("Axis");
                axis.SetAttributeValue("Name", workSheet.Cells[i + 2, 1].Value);
                axis.SetAttributeValue("Display", workSheet.Cells[i + 2, 2].Value);
                axis.SetAttributeValue("CardType", workSheet.Cells[i + 2, 3].Value);
                axis.SetAttributeValue("CardID", workSheet.Cells[i + 2, 4].Value);
                axis.SetAttributeValue("Index", workSheet.Cells[i + 2, 5].Value);
                axis.SetAttributeValue("Positive", workSheet.Cells[i + 2, 6].Value);
                axis.SetAttributeValue("Negative", workSheet.Cells[i + 2, 7].Value);
                axis.SetAttributeValue("CountPerMM", workSheet.Cells[i + 2, 8].Value);
                axis.SetAttributeValue("Direct", workSheet.Cells[i + 2, 9].Value);
                axis.SetAttributeValue("UseEncoder", workSheet.Cells[i + 2, 10].Value);



                XElement home = new XElement("homeParam");
                home.SetAttributeValue("Mode", workSheet.Cells[i + 2, 11].Value);
                home.SetAttributeValue("Direction", workSheet.Cells[i + 2, 12].Value);
                home.SetAttributeValue("Offset", workSheet.Cells[i + 2, 13].Value);
                home.SetAttributeValue("HomePosition", workSheet.Cells[i + 2, 14].Value);
                home.SetAttributeValue("StartSpeed", workSheet.Cells[i + 2, 15].Value);
                home.SetAttributeValue("Speed", workSheet.Cells[i + 2, 16].Value);
                home.SetAttributeValue("Acc", workSheet.Cells[i + 2, 17].Value);
                home.SetAttributeValue("LeaveHomeSpeed", workSheet.Cells[i + 2, 18].Value);

                axis.Add(home);
                axes.Add(axis);
            }


            XElement inputs = new XElement("InputIO");
            root.Add(inputs);

            var inputSheet = ep.Workbook.Worksheets["Inputs"];

            for (int i = 0; i < 120; i++) //inputSheet.Cells.Rows
            {
                if (inputSheet.Cells[i + 2, 1].Value == null ||
                    inputSheet.Cells[i + 2, 1].Value.ToString().Length < 1)
                {
                    continue;
                }

                XElement Line = new XElement("Input");
                Line.SetAttributeValue("name", inputSheet.Cells[i + 2, 1].Value);
                Line.SetAttributeValue("display", inputSheet.Cells[i + 2, 2].Value);
                Line.SetAttributeValue("cardtype", inputSheet.Cells[i + 2, 3].Value);
                Line.SetAttributeValue("cardid", inputSheet.Cells[i + 2, 4].Value);
                Line.SetAttributeValue("index", inputSheet.Cells[i + 2, 5].Value);
                Line.SetAttributeValue("port", inputSheet.Cells[i + 2, 6].Value);
                Line.SetAttributeValue("reversed", inputSheet.Cells[i + 2, 7].Value);

                inputs.Add(Line);
            }


            inputs = new XElement("OutputIO");
            root.Add(inputs);

            inputSheet = ep.Workbook.Worksheets["Outputs"];

            for (int i = 0; i < 120; i++) //inputSheet.Cells.Rows
            {
                if (inputSheet.Cells[i + 2, 1].Value == null ||
                    inputSheet.Cells[i + 2, 1].Value.ToString().Length < 1)
                {
                    continue;
                }

                XElement Line = new XElement("Output");
                Line.SetAttributeValue("name", inputSheet.Cells[i + 2, 1].Value);
                Line.SetAttributeValue("display", inputSheet.Cells[i + 2, 2].Value);
                Line.SetAttributeValue("cardtype", inputSheet.Cells[i + 2, 3].Value);
                Line.SetAttributeValue("cardid", inputSheet.Cells[i + 2, 4].Value);
                Line.SetAttributeValue("index", inputSheet.Cells[i + 2, 5].Value);
                Line.SetAttributeValue("port", inputSheet.Cells[i + 2, 6].Value);
                Line.SetAttributeValue("reversed", inputSheet.Cells[i + 2, 7].Value);

                inputs.Add(Line);
            }

            doc.Save(fileName);
        }

        #endregion
    }
}
