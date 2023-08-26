using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using IN3Automation.Utility;
using CommonUI;

namespace ProductSwitch
{
    public partial class Form1 : Form
    {
        private string _Path;
        private string _CurrentProduct;
        private string _SelectProduct;

        private IniFile _LastConfigFile = new IniFile();
        public Form1()
        {
            InitializeComponent();

            _Path = System.AppDomain.CurrentDomain.BaseDirectory;

            _LastConfigFile.FileName = Path.Combine(_Path, "Setting","LastConfigFile.ini");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshAvailable(comboBox1);
            RefreshCurrent(textBox1);
            RefreshAvailable(comboBox2);
            RefreshCurrent(textBox2);
       //     RefreshAvailable2(comboBox_VisionList);
        }

        void RefreshAvailable(ComboBox comb)
        {
            string[] folders = Directory.GetDirectories(Path.Combine(_Path, "Setting", "Products"));

            comb.Items.Clear();
            comb.Items.AddRange(folders.Select(f => Path.GetFileName(f)).ToArray());
        }

        void RefreshAvailable2(ComboBox comb)
        {
            string[] folders = Directory.GetDirectories(Path.Combine(_Path, "Setting", "VisionMode"));
            comb.Items.Clear();
            comb.Items.AddRange(folders.Select(f => Path.GetFileName(f)).ToArray());
        }

        void RefreshCurrent(TextBox textb)
        {
            //XDocument doc = XDocument.Load(Path.Combine(_Path, "AutomaticLabelAttach.exe.config"));

            //var pro = doc.Descendants("add").Where(e => e.Parent.Name == "appSettings");

            //foreach (var p in pro)
            //{
            //    if (p.Attribute("key").Value == "teach")
            //    {
            //        _CurrentTeach = p.Attribute("value").Value;
            //    }
            //    else if (p.Attribute("key").Value == "setting")
            //    {
            //        _CurrentApp = p.Attribute("value").Value;
            //    }
            //}

            //if (File.Exists(Path.Combine(_Path, "Setting", _CurrentApp)))
            //{
            //    doc = XDocument.Load(Path.Combine(_Path, "Setting", _CurrentApp));

            //    //get the product type name
            //    var pd = doc.Descendants("ProductName").FirstOrDefault();
            //    if (pd != null)
            //    {
            //        _CurrentProduct = pd.Value;
            //        textb.Text = pd.Value;
            //    }
            //}

            _CurrentProduct = _LastConfigFile.ReadString("System", "Product", "");
            textb.Text = _CurrentProduct;
        }

        void UpdateSetting()
        {

            //copy new
            File.Copy(Path.Combine(_Path, "Setting", "Products", _SelectProduct, "AppConfig.xml"), Path.Combine(_Path, "Setting", "AppConfig.xml"), true);
            File.Copy(Path.Combine(_Path, "Setting", "Products", _SelectProduct, "TeachData.xml"), Path.Combine(_Path, "Setting", "TeachData.xml"), true);


            //update LabelQuickAttach.exe.config
            XDocument doc = XDocument.Load(Path.Combine(_Path, "AutomaticLabelAttach.exe.config"));
            var pro = doc.Descendants("add").Where(e => e.Parent.Name == "appSettings");
            int c = pro.Count();
            for (int i = 0; i < c; i++)
            {
                if (pro.ElementAt(i).Attribute("key").Value == "teach")
                {
                    pro.ElementAt(i).SetAttributeValue("value", "TeachData.xml");
                }
                else if (pro.ElementAt(i).Attribute("key").Value == "setting")
                {
                    pro.ElementAt(i).SetAttributeValue("value", "AppConfig.xml");
                }
            }
        }


        #region 切换型号
        private void btnKill_Click_1(object sender, EventArgs e)
        {
            //Process[] label = Process.GetProcessesByName("AutomaticLabelAttach");

            //foreach (Process p in label)
            //{
            //    p.Kill();
            //    p.Close();
            //}
        }

        private void btnRefresh_Click_1(object sender, EventArgs e)
        {
            RefreshAvailable(comboBox1);
            RefreshCurrent(textBox1);
            RefreshAvailable(comboBox2);
            RefreshCurrent(textBox2);
        }

        private void btnSaveCurrent_Click_1(object sender, EventArgs e)
        {
            try
            {
                _SelectProduct = comboBox1.SelectedItem.ToString();
                if (_SelectProduct == null && _SelectProduct.Length < 1)
                    {
                    return;
                    }

                _CurrentProduct = _SelectProduct;
                if (!Directory.Exists(Path.Combine(_Path, "Setting", "Products", _CurrentProduct)))
                {
                    //  Directory.CreateDirectory(Path.Combine(_Path, "Products", _CurrentProduct));

                    MessageBox.Show("当前产品库中没有该产品参数配方，请先添加.");

                    return;
                }

                 _LastConfigFile.WriteString("System", "Product", _CurrentProduct);

                RefreshCurrent(textBox1);

                MessageBox.Show("当前配方名已经设置为： " + _SelectProduct);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败:\r\n" + ex.Message);
            }
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOK_Click_1(object sender, EventArgs e)
        {
            _SelectProduct = comboBox1.SelectedItem.ToString();
            if (_SelectProduct == null && _SelectProduct.Length < 1)
            {
                return;
            }
            if (MessageBox.Show("确定更新产品 " + _SelectProduct + " 吗？", "Confirm", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    UpdateSetting();
                    MessageBox.Show("更新成功！");
                    Process.Start("AutomaticLabelAttach.exe");
                    Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("更新失败:\r\n" + ex.Message);
                }
            }
        }
        #endregion

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            if (textBox_ProductSwitchName.Text != "" && comboBox2.Text != "")
            {
                string activeDir = _Path + "Setting/Products";
                string newPath = System.IO.Path.Combine(activeDir, textBox_ProductSwitchName.Text);

                if (Directory.Exists(Path.Combine(_Path, "Products", newPath)))
                    {
                    MessageBox.Show("配方库已经存在该配方，添加失败");
                    return;
                    }

               System.IO.Directory.CreateDirectory(newPath);// 创建了文件夹

                string OrigVisionPath = _Path + "Setting/Products/" + comboBox2.Text;
                CopyDir(OrigVisionPath, newPath);   //直接复制整个文件夹到目标位置

                MessageBox.Show("添加成功！");
            }
            else
            {
                MessageBox.Show("未添加型号名称或未选择相似型号,再或没有选择视觉类型模板,或者未选择料盒类型，请检查重新添加！");
            }
        }


        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("可选型号列表未选择，请重新选择！");
                return;
            }

            if (MessageBox.Show("删除后不可恢复,确定要删除配方： " + comboBox1.Text, "确认",
                MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }


            try
            {
                DeleteDir(Path.Combine(_Path , "Setting","Products",  comboBox1.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void DeleteDir(string aimPath)
        {
            try
                {
                //检查目标目录是否以目录分割字符结束如果不是则添加之  
                if (aimPath[aimPath.Length - 1] != Path.DirectorySeparatorChar)
                    aimPath += Path.DirectorySeparatorChar;
                //得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组  
                //如果你指向Delete目标文件下面的文件而不包含目录请使用下面的方法  
                //string[]fileList=  Directory.GetFiles(aimPath);  
                string[] fileList = Directory.GetFileSystemEntries(aimPath);
                //遍历所有的文件和目录   
                foreach (string file in fileList)
                    {
                    //先当作目录处理如果存在这个  
                    //目录就递归Delete该目录下面的文件   
                    if (Directory.Exists(file))
                        {
                        DeleteDir(aimPath + Path.GetFileName(file));
                        }
                    //否则直接Delete文件   
                    else
                        {
                        File.Delete(aimPath + Path.GetFileName(file));
                        }
                    }
                //删除文件夹   
                System.IO.Directory.Delete(aimPath, true);
                }
            catch (Exception e)
                {
                MessageBox.Show(e.ToString());
                }
            }

        /// <summary>
        /// 负责src 文件夹下所有文件及文件夹到目标文件夹 dest
        /// </summary>
        /// <param name="srcPath"></param>
        /// <param name="destPath"></param>
        public static void CopyDirectory(string srcPath, string destPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //获取目录下（不包含子目录）的文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)     //判断是否文件夹
                    {
                        if(i.Name != "backup")
                        {
                            if (!Directory.Exists(destPath + "\\" + i.Name))
                            {
                                Directory.CreateDirectory(destPath + "\\" + i.Name);   //目标目录下不存在此文件夹即创建子文件夹
                            }
                            System.IO.File.Copy(i.FullName, destPath + "\\" + i.Name);    //递归调用复制子文件夹
                        }
                    }
                    else
                    {
                        File.Copy(i.FullName, destPath + "\\" + i.Name, true);      //不是文件夹即复制文件，true表示可以覆盖同名文件
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }


        public static void CopyDir(string srcPath, string destPath)
            {
            if (Directory.Exists(srcPath) /*&& Directory.Exists(destPath)*/)
                {
                Directory.CreateDirectory(destPath);
                CopyDirectory(srcPath, destPath);
                }
            }
    }
}
