using Incube.Motion;
using Motion;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonUI
{
    public partial class TrayEditor : Form
    {
        #region Internal Class
        class TrayBlock
        {
            public RectangleF Pos { get; set; }

            public Point Loc { get; set; }

            public int Index { get; set; }

            public bool Activated { get; set; }
        }

        enum BlockSelection
        {
            None,
            Activate,
            Deactivate
        }
        #endregion

        private string _fileName = "";

        private const int BlockGap = 4;
        private const int TrayGap = 10;

        private BlockSelection _manualSelection = BlockSelection.Activate;
        private bool _writeSuccess;
        private List<TrayBlock> _blocks = new List<TrayBlock>();

        ComponentResourceManager resources;

        /// <summary>
        /// 模块的信息
        /// </summary>
        public List<PointDataElement> ModuleInfoList;

        /// <summary>
        /// 总点位信息
        /// </summary>
        public List<PointDataElement> TotalPointsInfoList;

        public IAxis axis_x;
        public IAxis axis_y;

        public string axisName_x;
        public string axisName_y;
        public string RefPointName;

        private int _totalRow;
        private int _totalCol;

        private int _selectEditColumnIndex;

        public TrayEditor()
        {         
            if (resources==null)
            {
                resources = new ComponentResourceManager(typeof(TrayEditor));
            }

            InitializeComponent();

            listViewModule.Items.Clear();
            listViewTotalInfo.Items.Clear();

            ModuleInfoList = new List<PointDataElement>();
            TotalPointsInfoList = new List<PointDataElement>();

            _writeSuccess = false;
        }

        public TrayEditor(string filename) : this()
        {
            _fileName = filename;
        
            this.Text = "TrayEditor - " + _fileName;

            if (filename.Length > 1)
            {
                LoadConfig();

                CreateBlock();

                pictureBox2.Invalidate();            
            }
        }


        private void NewFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _fileName = "";
            this.Text = "TrayEditor - " + _fileName;

            numericUpDownTrayCol.Value = numericUpDownTrayRow.Value = 1;

            checkBoxValidAll.Checked = true;
        }

        private void OpenFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Tray Configuration File (*.json)|*.json";

            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _fileName = openFileDialog1.FileName;
            this.Text = "TrayEditor - " + _fileName;

            LoadConfig();
         //   pictureBox1.Invalidate();
        }

        private void FileSaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Tray Configuration File (*.json)|*.json";
            saveFileDialog1.FileName = "Traymap.json";

            if (saveFileDialog1.ShowDialog() != DialogResult.OK )
            {
                return;
            }

            _fileName = saveFileDialog1.FileName;
            this.Text = "TrayEditor - " + _fileName;

            WriteConfigToFile();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (_fileName.Length < 1)
            {
                FileSaveAsToolStripMenuItem_Click(sender, e);
                return;
            }

            UpdataTotalPointsList();

            WriteConfigToFile();

            if (_writeSuccess)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                return;
            }
            this.Close();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #region 信息总列表 相关操作

        private void RefreshTotalInfoList()
        {
            listViewTotalInfo.Items.Clear();
            foreach (var m in TotalPointsInfoList)
            {
                ListViewItem lvi = new ListViewItem((m.Index).ToString());

                string[] coluums = new string[] 
                {
                    m.X.ToString("F3"), m.Y.ToString("F3"), m.R.ToString("F2"), m.Name,
                    m.row.ToString(),m.col.ToString(),m.BoardIndex.ToString(),m.Active.ToString(),
                };

                lvi.SubItems.AddRange(coluums);

                listViewTotalInfo.Items.Add(lvi);
            }
        }

        private void AddTotalInfo()
        {
            int row = (int)numericUpDownTrayRow.Value;
            int col = (int)numericUpDownTrayCol.Value;

            double pitch_x = double.Parse(textBoxTrayPitch_x.Text);
            double pitch_y = double.Parse(textBoxTrayPitch_y.Text);

            bool sLoop = checkBoxSLoop.Checked;

            //一个小模块的产品，每一个产品当成一列，不管里边有多少个产品，后期需要改善
          //  _totalCol += col * ModuleInfoList.Count; 
            _totalRow += row;

            int begincol = 0;
            int beginrow = 0;
            int beginboard = 0;

            if(TotalPointsInfoList.Count > 0)
            {
             //   begincol = TotalPointsInfoList.Max(m => m.col) + 1;  //多次添加拼版只能 同行同列
                beginrow = TotalPointsInfoList.Max(m => m.row) + 1;
                beginboard = TotalPointsInfoList.Max(m => m.BoardIndex) + 1;
            }
            else
            {
                _totalCol += col * ModuleInfoList.Count;
            }

            for (int r = 0;r < row;r ++)
            {
                for(int c = 0; c < col;c++)
                {
                    if ((r + 1) % 2 == 0 && sLoop)  //偶数行
                    {
                        GenerateTray(r,col- c - 1, pitch_x, pitch_y, begincol + (col - c - 1) * ModuleInfoList.Count, beginrow + r, beginboard);
                    }
                    else
                    {
                        GenerateTray(r, c, pitch_x, pitch_y,begincol + c * ModuleInfoList.Count, beginrow + r,beginboard);
                    }

                 //   beginboard++;
                }
            }

        }

        private void GenerateTray(int rowIndex,int colIndex, double pitch_x,double pitch_y,int begincol,int beginrow,int beginboard)
        {
            int colIncrease = 0;

            foreach (var m in ModuleInfoList)
            {
                PointDataElement p = new PointDataElement()
                {
                    Index = TotalPointsInfoList.Count,
                    X = m.X + colIndex * pitch_x,
                    Y = m.Y + rowIndex * pitch_y,
                    R = m.R,
                    Name = m.Name,
                    col = begincol + colIncrease,
                    row = beginrow,
                    BoardIndex = beginboard,
                    Active = 1,
                };

                TotalPointsInfoList.Add(p);

                colIncrease++;
            }

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {         
            UpdataModulList();

            AddTotalInfo();

            RefreshTotalInfoList();

            CreateBlock();

            pictureBox2.Invalidate();
        }

        private void CreateBlock()
        {
            int picWidth = pictureBox2.ClientRectangle.Width;
            int picHeight = pictureBox2.ClientRectangle.Height;
            float w = 1.0f * (picWidth - BlockGap * (_totalCol - 1)) / (_totalCol);
            float h = 1.0f * (picHeight - BlockGap * (_totalRow - 1)) / (_totalRow);

            _blocks.Clear();
            foreach (var p in TotalPointsInfoList)
            {
                TrayBlock block = new TrayBlock() { Activated = true };
                block.Index = p.Index;
                block.Loc = new Point(p.col, p.row);
                block.Pos = new RectangleF(block.Loc.X * (w + BlockGap), block.Loc.Y * (h + BlockGap), w, h);
                _blocks.Add(block);
            }

        }

        /// <summary>
        /// 更新模块信息列表
        /// </summary>
        private void UpdataModulList()
        {
            ModuleInfoList.Clear();

            for (int i = 0;i< listViewModule.Items.Count;i++)
            {
                PointDataElement data = new PointDataElement
                {
                    Index = int.Parse(this.listViewModule.Items[i].SubItems[0].Text),
                    X = double.Parse(this.listViewModule.Items[i].SubItems[1].Text),
                    Y = double.Parse(this.listViewModule.Items[i].SubItems[2].Text),
                    R = double.Parse(this.listViewModule.Items[i].SubItems[3].Text),
                    Name = this.listViewModule.Items[i].SubItems[4].Text,
                };

                ModuleInfoList.Add(data);
            }

            RefreshModuleData();
        }

        /// <summary>
        /// 更新总产品信息列表
        /// </summary>
        private void UpdataTotalPointsList()
        {
            TotalPointsInfoList.Clear();

            for (int i = 0; i < listViewTotalInfo.Items.Count; i++)
            {
                PointDataElement data = new PointDataElement
                {
                    Index = int.Parse(this.listViewTotalInfo.Items[i].SubItems[0].Text),
                    X = double.Parse(this.listViewTotalInfo.Items[i].SubItems[1].Text),
                    Y = double.Parse(this.listViewTotalInfo.Items[i].SubItems[2].Text),
                    R = double.Parse(this.listViewTotalInfo.Items[i].SubItems[3].Text),
                    Name = this.listViewTotalInfo.Items[i].SubItems[4].Text,
                    row = int.Parse(this.listViewTotalInfo.Items[i].SubItems[5].Text),
                    col = int.Parse(this.listViewTotalInfo.Items[i].SubItems[6].Text),
                    BoardIndex = int.Parse(this.listViewTotalInfo.Items[i].SubItems[7].Text),
                    Active = int.Parse(this.listViewTotalInfo.Items[i].SubItems[8].Text),
                };

                TotalPointsInfoList.Add(data);
            }

            RefreshTotalInfoList();
        }

        private void ResetTotalListIndex()
        {
            List<PointDataElement> buffer = new List<PointDataElement>();
            foreach (var d in TotalPointsInfoList)
            {
                buffer.Add(d);              
            }

            TotalPointsInfoList.Clear();

            foreach(var l in buffer)
            {
                l.Index = TotalPointsInfoList.Count;

                TotalPointsInfoList.Add(l);
            }
        }

        private void btnDeleteBoard_Click(object sender, EventArgs e)
        {
            string str = ((Button)sender).Tag.ToString();

            if (str == "All")
            {
                if (DialogResult.Yes != MessageBox.Show("是否删除全部数据？？", "删除数据", MessageBoxButtons.YesNo)) return;

                TotalPointsInfoList.Clear();

                _totalRow = _totalCol = 0;

                CreateBlock();

                pictureBox2.Invalidate();
            }
            else if (str == "S") //删除选定的数据点，因Tray以行列为记录位置基础，删除导致行列不对称，会有bug
            {
                if (DialogResult.Yes != MessageBox.Show("是否删除选定数据？？", "删除数据", MessageBoxButtons.YesNo)) return;

                List<int> deleteArray = new List<int>();
                foreach (ListViewItem item in this.listViewTotalInfo.SelectedItems)
                {
                    string id = item.SubItems[0].Text;

                    foreach (var d in TotalPointsInfoList)
                    {
                        if (id == d.Index.ToString())
                        {
                            deleteArray.Add(d.Index);
                        }
                    }
                }

                foreach (var dd in deleteArray)
                {
                    var fd = TotalPointsInfoList.FirstOrDefault(t => t.Index == dd);

                    if (fd != null)
                    {
                        TotalPointsInfoList.Remove(fd);
                    }
                }

                ResetTotalListIndex();
            }           

            RefreshTotalInfoList();          
        }

        private void listViewTotalInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listViewTotalInfo.SelectedItems.Count > 0)
            {
                btnListToModule.Visible = true;
            }
            else
            {
                btnListToModule.Visible = false;
            }
        }

        #endregion

        #region Checkbox and radio box


        private void SetAllAcivated()
        {
            for (int i = 0; i < TotalPointsInfoList.Count; i++)
            {
               TotalPointsInfoList[i].Active = 1;
            }

            CreateBlock();

            RefreshTotalInfoList();
        }

        private void checkBoxValidAll_CheckedChanged(object sender, EventArgs e)
        {
       //     groupBoxValid.Enabled = !checkBoxValidAll.Checked;

            if (checkBoxValidAll.Checked)
            {
                _manualSelection = BlockSelection.Deactivate;
                SetAllAcivated();
                pictureBox2.Invalidate();
            }
            else
            {
                _manualSelection = BlockSelection.Deactivate;
                radioButtonInvalid.Checked = true;
                radioButtonValid.Checked = false;
            }
        }


        private void radioButtonValid_CheckedChanged(object sender, EventArgs e)
        {
            _manualSelection = radioButtonValid.Checked ? BlockSelection.Activate : BlockSelection.Deactivate;

         //   pictureBox1.Invalidate();
        }


        #endregion

        #region Listview item editor, 修改操作

        private void checkBoxEditModul_CheckedChanged(object sender, EventArgs e)
        {
            textBoxModulValue.Visible = textBoxTotalInfo.Visible = false;
        }

        private void listViewModule_MouseClick(object sender, MouseEventArgs e)
        {
            var listview = (ListView)sender;

            var loc = listview.Location;
            var item = listview.GetItemAt(e.X, e.Y);
            if (item == null || e.X < listview.Columns[0].Width)
            {
                return;
            }

            TextBox tbox = new TextBox();
            if(listview == listViewModule)
            {
                if (!checkBoxEditModul.Checked) return;
                tbox =  textBoxModulValue ;
            }
            else
            {
                if (!checkBoxEditTotal.Checked) return;
                tbox =  textBoxTotalInfo;
            }         
            
            _selectEditColumnIndex = -1;
            for (int i = 0;i < item.SubItems.Count;i++)
            {
                int xpos = item.SubItems[i].Bounds.X;

                if(xpos + loc.X > e.X)
                {
                    _selectEditColumnIndex = i - 1;
                    break;
                }
            }
            if (_selectEditColumnIndex == -1) _selectEditColumnIndex = item.SubItems.Count - 1;
            if(_selectEditColumnIndex == 0 || (_selectEditColumnIndex > 4 && _selectEditColumnIndex != 8)) //0,5,6,7 不允许编辑
            {
                tbox.Visible = false;return;
            }

            int w = listview.Columns[_selectEditColumnIndex].Width;
            var sub = item.SubItems[_selectEditColumnIndex];

            tbox.Tag = sub;
            tbox.Text = sub.Text;
            tbox.Location = new Point(loc.X + sub.Bounds.X + 4, loc.Y + sub.Bounds.Y);
            tbox.Visible = true;
            tbox.BringToFront();
            tbox.Focus();           
        }

        private void textBoxModulValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            var text = (TextBox)sender;

            if (e.KeyChar == '\r')
            {
                ((ListViewItem.ListViewSubItem)text.Tag).Text = text.Text;
                text.Visible = false;
            }
            else if (e.KeyChar == 27)
            {
                text.Visible = false; //ESC
            }
            else if ((e.KeyChar > '9' || e.KeyChar < '0') && e.KeyChar != '.' && e.KeyChar != '\b' && _selectEditColumnIndex != 4) //backspace
            {
                e.Handled = true;
            }

        }

        private void textBoxValueEditor_Leave(object sender, EventArgs e)
        {
            var text = (TextBox)sender;

            ((ListViewItem.ListViewSubItem)text.Tag).Text = text.Text;

            text.Visible = false;
        }
        #endregion

        #region 方框绘制


        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            //if (_rows < 1 || _cols < 1)
            //{
            //    return;
            //}

            var activated = _blocks.Where(b => b.Activated).Select(b => b.Pos).ToArray();
            var deactivated = _blocks.Where(b => !b.Activated).Select(b => b.Pos).ToArray();

            if (activated.Length > 0)
            {
                e.Graphics.FillRectangles(Brushes.Gold, activated);
            }

            if (deactivated.Length > 0)
            {
                e.Graphics.FillRectangles(Brushes.Gray, deactivated);
            }

            Font f = new Font("Arial", 10);
            foreach (var block in _blocks)
            {
                e.Graphics.DrawString(block.Index.ToString(), f, Brushes.RoyalBlue, block.Pos.Location);
            }
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (_manualSelection == BlockSelection.None)
            {
                return;
            }

            TrayBlock actBlock = null;
            foreach (var block in _blocks)
            {
                if (block.Pos.Contains(e.X, e.Y))
                {
                    actBlock = block;
                    break;
                }
            }

            if (actBlock == null)
            {
                return;
            }


            switch (_manualSelection)
            {
                case BlockSelection.None:
                    break;
                case BlockSelection.Activate:
                    actBlock.Activated = true;
                    UpdataActiveByIndex(actBlock.Index, true);
                    break;
                case BlockSelection.Deactivate:
                    actBlock.Activated = false;
                    UpdataActiveByIndex(actBlock.Index, false);
                    break;
                default:
                    break;
            }

           pictureBox2.Invalidate();
        }

        private void UpdataActiveByIndex(int index,bool bAcitvated)
        {
            for(int i = 0;i<TotalPointsInfoList.Count;i++)
            {
                if (TotalPointsInfoList[i].Index == index)
                {
                    TotalPointsInfoList[i].Active = bAcitvated ? 1 : 0;

                    break;
                }
            }

            RefreshTotalInfoList();
        }

        #endregion


        #region 模块操作

        /// <summary>
        /// 获取位置
        /// </summary>
        private void GetRealPos()
        {
            double ref_x = double.Parse(textBoxRefPos_x.Text);
            double ref_y = double.Parse(textBoxRefPos_y.Text);

            double real_x = 0, real_y = 0;
            if (axis_x != null)
            {
                real_x = axis_x.Position - real_x;
            }
            if (axis_y != null)
            {
                real_y = axis_y.Position - real_y;
            }

            textBoxRealPos_x.Text = real_x.ToString("F3");
            textBoxRealPos_y.Text = real_y.ToString("F3");
        }
        private void RefreshModuleData()
        {
            listViewModule.Items.Clear();
            foreach (var m in ModuleInfoList)
            {
                ListViewItem lvi = new ListViewItem((m.Index).ToString());

                string[] coluums = new string[] { m.X.ToString("F3"), m.Y.ToString("F3"), m.R.ToString("F3"), m.Name };

                lvi.SubItems.AddRange(coluums);

                listViewModule.Items.Add(lvi);
            }
        }

        private void AddModuleInfo()
        {
            double realpos_x = double.Parse(textBoxRealPos_x.Text);
            double realpos_y = double.Parse(textBoxRealPos_y.Text);
            double angle = double.Parse(textBoxRealPos_r.Text);

            string name = textBoxName.Text;

            PointDataElement p = new PointDataElement()
            {
                Index = ModuleInfoList.Count,
                X = realpos_x,
                Y = realpos_y,
                R = angle,
                Name = name,
            };

            ModuleInfoList.Add(p);
        }

        private void btnGetRealPos_Click(object sender, EventArgs e)
        {
            GetRealPos();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(radioButtonOnline.Checked)
            {
                GetRealPos();
            }

            AddModuleInfo();

            RefreshModuleData();
        }

        private void btnDeleteModule_Click(object sender, EventArgs e)
        {
            string str = ((Button)sender).Tag.ToString();

            if(str == "All")
            {
                if (DialogResult.Yes != MessageBox.Show("是否删除全部数据", "删除数据", MessageBoxButtons.YesNo)) return;

                ModuleInfoList.Clear();
            }
            else if(str == "S")
            {
                if (DialogResult.Yes != MessageBox.Show("是否删除选定的数据", "删除数据", MessageBoxButtons.YesNo)) return;

                List<int> deleteArray = new List<int>();
                foreach (ListViewItem item in this.listViewModule.SelectedItems)
                {
                    string id = item.SubItems[0].Text;

                    foreach(var d in ModuleInfoList)
                    {
                        if(id == d.Index.ToString())
                        {
                            deleteArray.Add(d.Index);
                        }
                    }
                }

                foreach(var dd in deleteArray)
                {
                    var fd =  ModuleInfoList.FirstOrDefault(t=>t.Index == dd);

                    if(fd != null)
                    {
                        ModuleInfoList.Remove(fd);
                    }
                }
            }

            RefreshModuleData();
        }

        private void listViewModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.listViewModule.SelectedItems.Count == 1)
            {
                textBoxRealPos_x.Text = this.listViewModule.SelectedItems[0].SubItems[1].Text;
                textBoxRealPos_y.Text = this.listViewModule.SelectedItems[0].SubItems[2].Text;
                textBoxRealPos_r.Text = this.listViewModule.SelectedItems[0].SubItems[3].Text;
                textBoxName.Text = this.listViewModule.SelectedItems[0].SubItems[4].Text;
            }
        }

        private void btnListToModule_Click(object sender, EventArgs e)
        {
            string str = "是否把选定的数据添加到模块？ 确定请选择【是】，否则选择【否】 \r\n";
            foreach (ListViewItem item in this.listViewTotalInfo.SelectedItems)
            {             
                str += $"index = {item.SubItems[0].Text},x = {item.SubItems[1].Text},y = {item.SubItems[2].Text},r = {item.SubItems[3].Text},name = {item.SubItems[4].Text}";                               
                str += "\r\n";
            }

            if(DialogResult.Yes == MessageBox.Show(str,"转化为模块",MessageBoxButtons.YesNo))
            {
                foreach (ListViewItem item in this.listViewTotalInfo.SelectedItems)
                {
                    PointDataElement p = new PointDataElement()
                    {
                        Index = ModuleInfoList.Count,
                        X = double.Parse(item.SubItems[1].Text),
                        Y = double.Parse(item.SubItems[2].Text),
                        R = double.Parse(item.SubItems[3].Text),
                        Name = item.SubItems[4].Text,
                    };

                    ModuleInfoList.Add(p);
                }

                RefreshModuleData();
            }

            btnListToModule.Visible = false;
        }

        #endregion

        private void WriteConfigToFile()
        {
            int row = (int)numericUpDownTrayRow.Value;
            int col = (int)numericUpDownTrayCol.Value;

            double pitch_x = double.Parse(textBoxTrayPitch_x.Text);
            double pitch_y = double.Parse(textBoxTrayPitch_y.Text);

            bool sLoop = checkBoxSLoop.Checked;
            bool leftToRight = checkBoxLeftToRight.Checked;

            using (var file = new FileStream(_fileName, FileMode.Create, FileAccess.Write))
            {
                var sw = new StreamWriter(file);

                jsonFile saveObject = new jsonFile
                {
                    id = _fileName,
                    axisName_x = axisName_x,
                    axisName_y = axisName_y,
                    RefPoint = RefPointName,
                    s_loop = sLoop,
                    left_to_right = leftToRight,
                    pitch_x = pitch_x,
                    pitch_y = pitch_y,
                    rowIndex = row,
                    colIndex = col,
                    module = ModuleInfoList,
                    total = TotalPointsInfoList,
                    totalRow = _totalRow,
                    totalCol = _totalCol,
                };

                var data = JsonConvert.SerializeObject(saveObject, Formatting.Indented);
                sw.WriteLine(data);

                sw.Close();
            }

            _writeSuccess = true;
        }


        private void LoadConfig()
        {
            using (var file = new FileStream(_fileName, FileMode.Open, FileAccess.Read))
            {
                var sr = new StreamReader(file);
                var setting = JsonConvert.DeserializeObject<jsonFile>(sr.ReadToEnd());

                axisName_x = setting.axisName_x;
                axisName_y = setting.axisName_y;
                RefPointName = setting.RefPoint;
                checkBoxSLoop.Checked = setting.s_loop;
                checkBoxLeftToRight.Checked = setting.left_to_right;
                numericUpDownTrayRow.Value = setting.rowIndex;
                numericUpDownTrayCol.Value = setting.colIndex;
                textBoxTrayPitch_x.Text = setting.pitch_x.ToString("F3");
                textBoxTrayPitch_y.Text = setting.pitch_y.ToString("F3");

                _totalRow = setting.totalRow;
                _totalCol = setting.totalCol;

                ModuleInfoList.Clear();
                TotalPointsInfoList.Clear();

                ModuleInfoList = setting.module;
                TotalPointsInfoList = setting.total;
            }

            RefreshModuleData();
            RefreshTotalInfoList();
        }

    }
}
