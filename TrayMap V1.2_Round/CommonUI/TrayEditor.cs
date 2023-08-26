using Incube.Motion;
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

        private BlockSelection _manualSelection;
        private int _rows, _cols;
        private int _arrayRows, _arrayCols;
        private bool _traySLoop, _trayLeftToRight, _arrayRowFirst,_writeSuccess;
        private List<TrayBlock> _blocks = new List<TrayBlock>();
        private TrayBlock[,] _trayMap = null;

        ComponentResourceManager resources;

        public string FileName { get { return _fileName; } }

        public TrayEditor()
        {
            
            if (resources==null)
            {
                resources = new ComponentResourceManager(typeof(TrayEditor));
            }

            InitializeComponent();

            listViewDistance.Items.Clear();
            listViewRotation.Items.Clear();

            _rows = _cols = 0;
            _arrayCols = _arrayRows = 1;
            _traySLoop = false;
            _trayLeftToRight = _arrayRowFirst = true;
            _writeSuccess = false;
        }

        public TrayEditor(string filename) : this()
        {
            _fileName = filename;
            if(_fileName.Contains("Feeder"))
            {
                checkBoxMultiArray.Visible = false;
                groupBoxTrayArray.Visible = false;
                groupBoxRotation.Visible = false;
                checkBoxRotation.Visible = false;
            }
            else if(_fileName.Contains("Plate"))
            {
                //groupBoxRotation.Visible = false;
                //checkBoxRotation.Visible = false;
            }
            else if(_fileName.Contains("Stack"))
            {
                checkBoxMultiArray.Enabled = true;
                groupBoxTrayArray.Enabled = true;
                groupBoxRotation.Enabled = true;
                checkBoxRotation.Enabled = true;
                //groupBox1.Visible = false;
                //groupBox2.Visible = false;
                //checkBoxSamePitch.Visible = false;
            }
            this.Text = "TrayEditor - " + _fileName;

            if (filename.Length > 1)
            {
                LoadConfig();
                pictureBox1.Invalidate();
            }
        }


        private void NewFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _fileName = "";
            this.Text = "TrayEditor - " + _fileName;

            textBoxTrayName.Text = "";
            numericUpDownTrayCol.Value = numericUpDownTrayRow.Value = 1;
            numericUpDownPitchY.Value = numericUpDownPitchX.Value = 1;
            numericUpDownArrayCol.Value = numericUpDownArrayRow.Value = 1;

            checkBoxLeftToRight.Checked = checkBoxSamePitch.Checked = true;
            checkBoxMultiArray.Checked = checkBoxRotation.Checked = false;
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
            pictureBox1.Invalidate();
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

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (textBoxTrayName.Text.Length < 1)
            {
                MessageBox.Show(resources.GetString("TrayName"));
                textBoxTrayName.Focus();

                return;
            }

            UpdateSetting();

            pictureBox1.Invalidate();
        }

        #region Checkbox and radio box
        private void checkBoxSamePitch_CheckedChanged(object sender, EventArgs e)
        {
            panelDistance.Visible = checkBoxSamePitch.Checked;
            listViewDistance.Visible = !checkBoxSamePitch.Checked;

            if (!checkBoxSamePitch.Checked)
            {
                int row = (int)numericUpDownTrayRow.Value;
                int col = (int)numericUpDownTrayCol.Value;

                col = col > row ? col : row;

                for (int i = listViewDistance.Items.Count; i < col - 1; i++)
                {
                    ListViewItem lvi = new ListViewItem((i + 1).ToString());
                    lvi.SubItems.AddRange(new string[] { "0.00", "0.00" });

                    listViewDistance.Items.Add(lvi);
                }

                //delete more
                for (int i = listViewDistance.Items.Count - 1; i > col - 2; i--)
                {
                    listViewDistance.Items.RemoveAt(i);
                }

            }
        }

        private void checkBoxValidAll_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxValid.Enabled = !checkBoxValidAll.Checked;

            if (checkBoxValidAll.Checked)
            {
                _manualSelection = BlockSelection.None;
                pictureBox1.Invalidate();
            }
            else
            {
                _manualSelection = BlockSelection.Deactivate;
                radioButtonInvalid.Checked = true;
                radioButtonValid.Checked = false;
            }
        }

        private void checkBoxRotation_CheckedChanged(object sender, EventArgs e)
        {
          //  groupBoxRotation.Enabled = checkBoxRotation.Checked;
            listViewRotation.Visible = checkBoxRotation.Checked;
            textBox_SingleAngel.Visible = !checkBoxRotation.Checked;
            checkBox_SingleRotation.Visible=!checkBoxRotation.Checked;

            if (!checkBoxRotation.Checked)
            {
                return;
            }

            radioButtonRotateRow_CheckedChanged(null, null);
        }

        private void radioButtonValid_CheckedChanged(object sender, EventArgs e)
        {
            _manualSelection = radioButtonValid.Checked ? BlockSelection.Activate : BlockSelection.Deactivate;

            pictureBox1.Invalidate();
        }

        private void checkBoxMultiArray_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxTrayArray.Enabled = checkBoxMultiArray.Checked;
        }

        private void radioButtonRotateRow_CheckedChanged(object sender, EventArgs e)
        {
            int count = (int)numericUpDownTrayRow.Value;
            int col = (int)numericUpDownTrayCol.Value;
            if (radioButtonRotateCol.Checked)
            {
                count = col;
            }

            for (int i = listViewRotation.Items.Count; i < count; i++)
            {
                ListViewItem lvi = new ListViewItem((i + 1).ToString());
                lvi.SubItems.AddRange(new string[] { "0.00" });

                listViewRotation.Items.Add(lvi);
            }

            //delete more
            for (int i = listViewRotation.Items.Count - 1; i > count - 1; i--)
            {
                listViewRotation.Items.RemoveAt(i);
            }
        }

        private void InitListviewName()
        {
            if (checkBoxNameEdit.Checked && !radioButtonSameName_all.Checked)
            {
                int row = (int)numericUpDownTrayRow.Value;
                int col = (int)numericUpDownTrayCol.Value;

                int num = radioButtonSameName_column.Checked ? col : row;

                if(DialogResult.OK == MessageBox.Show("当前存在数据，是否清除 ？","清除确认",MessageBoxButtons.OKCancel,MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                {
                    listViewName.Items.Clear();
                }

                for (int i = listViewName.Items.Count; i > num; i = listViewName.Items.Count)
                {
                    listViewName.Items.RemoveAt(i -1);
                }

                for (int i = listViewName.Items.Count; i < num; i++)
                {
                    ListViewItem lvi = new ListViewItem((i + 1).ToString());

                    lvi.SubItems.AddRange(new string[] { "L" });

                    listViewName.Items.Add(lvi);
                }
            }
        }

        #endregion

        #region Listview item editor, 间距修改操作
        private void listViewDistance_Leave(object sender, EventArgs e)
        {
            return;// 一旦textbox被focus了，那么久触发了这个事件，所以不能用

            //var textbox = textBoxRotation;
            //if (sender == listViewDistance)
            //{
            //    textbox = textBoxValueEditor;
            //}

            //if (textbox.Visible)
            //{
            //    ((ListViewItem.ListViewSubItem)textbox.Tag).Text = textbox.Text;

            //    textbox.Visible = false;
            //}
        }

        private void listViewDistance_MouseClick(object sender, MouseEventArgs e)
        {
            var listview = (ListView)sender;

            var loc = listview.Location;
            var item = listview.GetItemAt(e.X, e.Y);
            if (item == null || e.X < listview.Columns[0].Width)
            {
                return;
            }

            if (listview == listViewDistance)
            {
                int index = 1;
                if (e.X - loc.X - columnHeaderIndex.Width - columnHeaderRow.Width > 0)
                {
                    index = 2;
                }

                int w = listview.Columns[index].Width;
                var sub = item.SubItems[index];

                textBoxValueEditor.Tag = sub;
                textBoxValueEditor.Text = sub.Text;
                textBoxValueEditor.Location = new Point(loc.X + sub.Bounds.X + 4, loc.Y + sub.Bounds.Y);
                textBoxValueEditor.Visible = true;
                textBoxValueEditor.BringToFront();
                textBoxValueEditor.Focus();
            }
            else if (listview == listViewRotation)
            {
                int index = 1;

                int w = listview.Columns[index].Width;
                var sub = item.SubItems[index];

                textBoxRotation.Tag = sub;
                textBoxRotation.Text = sub.Text;
                textBoxRotation.Width = w;
                textBoxRotation.Location = new Point(loc.X + sub.Bounds.X + 4, loc.Y + sub.Bounds.Y);
                textBoxRotation.Visible = true;
                textBoxRotation.BringToFront();
                textBoxRotation.Focus();
            }
            else if (listview == listViewName)
            {
                int index = 1;

                int w = listview.Columns[index].Width;
                var sub = item.SubItems[index];

                textBoxNameEdit.Tag = sub;
                textBoxNameEdit.Text = sub.Text;
                textBoxNameEdit.Width = w;
                textBoxNameEdit.Location = new Point(loc.X + sub.Bounds.X + 4, loc.Y + sub.Bounds.Y);
                textBoxNameEdit.Visible = true;
                textBoxNameEdit.BringToFront();
                textBoxNameEdit.Focus();
            }
        }

        private void textBoxValueEditor_KeyPress(object sender, KeyPressEventArgs e)
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
            else if ((e.KeyChar > '9' || e.KeyChar < '0') && e.KeyChar != '.' && e.KeyChar != '\b') //backspace
            {
                e.Handled = true;
            }

        }

        private void textBoxNameEditor_KeyPress(object sender, KeyPressEventArgs e)
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
            //else if ((e.KeyChar > '9' || e.KeyChar < '0') && e.KeyChar != '.' && e.KeyChar != '\b') //backspace
            //{
            //    e.Handled = true;
            //}

        }

        private void textBoxValueEditor_Leave(object sender, EventArgs e)
        {
            var text = (TextBox)sender;

            ((ListViewItem.ListViewSubItem)text.Tag).Text = text.Text;

            text.Visible = false;
        }
        #endregion



        #region 方框绘制
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (_rows < 1 || _cols < 1)
            {
                return;
            }

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

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
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
                    break;
                case BlockSelection.Deactivate:
                    actBlock.Activated = false;
                    break;
                default:
                    break;
            }

            pictureBox1.Invalidate();
        }

        #endregion



        private void checkBoxNameEdit_CheckedChanged(object sender, EventArgs e)
        {
            EnableNameList(checkBoxNameEdit.Checked);

            InitListviewName();            
        }

        private void EnableNameList(bool bshow)
        {
            radioButtonSameName_row.Visible = !bshow;
            radioButtonSameName_column.Visible = !bshow;
            radioButtonSameName_all.Visible = !bshow;
            textBoxSameName.Visible = !bshow;

            listViewName.Visible = bshow && !radioButtonSameName_all.Checked;
        }


        private void UpdateSetting()
        {
            string id = textBoxTrayName.Text;

            int row = (int)numericUpDownTrayRow.Value;
            int col = (int)numericUpDownTrayCol.Value;
            int arrayRow = (int)numericUpDownArrayRow.Value;
            int arrayCol = (int)numericUpDownArrayCol.Value;

            bool sLoop = checkBoxSLoop.Checked;
            bool leftToRight = checkBoxLeftToRight.Checked;
            bool arrayRowFirst = radioButtonArrayRow.Checked;


            int picWidth = pictureBox1.ClientRectangle.Width;
            int picHeight = pictureBox1.ClientRectangle.Height;

            if (row != _rows || col != _cols || arrayCol != _arrayCols || arrayRow != _arrayRows
                || sLoop != _traySLoop || leftToRight != _trayLeftToRight || arrayRowFirst != _arrayRowFirst)
            {
                _blocks.Clear();
                _trayMap = new TrayBlock[row * arrayRow, col * arrayCol];

                _rows = row;
                _cols = col;
                _arrayCols = arrayCol;
                _arrayRows = arrayRow;

                _traySLoop = sLoop;
                _trayLeftToRight = leftToRight;
                _arrayRowFirst = arrayRowFirst;

                float w = 1.0f * (picWidth - BlockGap * (col * arrayCol - 1) - TrayGap * (arrayCol - 1)) / (col * arrayCol);
                float h = 1.0f * (picHeight - BlockGap * (row * arrayRow - 1) - TrayGap * (arrayRow - 1)) / (row * arrayRow);

                if (_arrayRowFirst)
                {
                    for (int i = 0; i < arrayRow; i++)
                    {
                        for (int j = 0; j < arrayCol; j++)
                        {
                            int index = (i * arrayCol + j) * row * col;
                            GenerateTray(row, col, sLoop, w, h, i, j, index);
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < arrayCol; j++)
                    {
                        for (int i = 0; i < arrayRow; i++)
                        {
                            int index = (j * arrayRow + i) * row * col;
                            GenerateTray(row, col, sLoop, w, h, i, j, index);
                        }
                    }
                }

                
            }
        }

        private void GenerateTray(int row, int col, bool sLoop, float w, float h, int i, int j, int index)
        {
            for (int m = 0; m < row; m++)
            {

                for (int n = 0; n < col; n++)
                {
                    TrayBlock block = new TrayBlock() { Activated = true };
                    if (_trayLeftToRight)
                    {
                        if (sLoop)
                        {
                            if (m % 2 == 0)
                            {
                                block.Index = index + m * col + n;
                            }
                            else
                            {
                                block.Index = index + (m + 1) * col - n - 1;
                            }
                        }
                        else
                        {
                            block.Index = index + m * col + n;
                        }
                    }
                    else
                    {
                        if (sLoop)
                        {
                            if (m % 2 == 1)
                            {
                                block.Index = index + m * col + n;
                            }
                            else
                            {
                                block.Index = index + (m + 1) * col - n - 1;
                            }
                        }
                        else
                        {
                            block.Index = index + (m + 1) * col - n - 1;
                        }
                    }

                    block.Loc = new Point(x: j * col + n,y: i * row + m);
                    block.Pos = new RectangleF(
                        block.Loc.X * (w + BlockGap) + block.Loc.X / col * TrayGap,
                        block.Loc.Y * (h + BlockGap) + block.Loc.Y / row * TrayGap,
                        w, h
                        );

                    _blocks.Add(block);
                    _trayMap[block.Loc.Y, block.Loc.X] = block;
                }
            }
        }

        private void WriteConfigToFile()
        {
            string id = textBoxTrayName.Text;

            int row = (int)numericUpDownTrayRow.Value;
            int col = (int)numericUpDownTrayCol.Value;
            bool sLoop = checkBoxSLoop.Checked;
            bool leftToRight = checkBoxLeftToRight.Checked;
            
            


            //tray pitch
            bool samePitch = checkBoxSamePitch.Checked;
            var trayPitchX = new List<float>();
            var trayPitchY = new List<float>();
            if (samePitch)
            {
                trayPitchX.Add((float)numericUpDownPitchY.Value);
                trayPitchY.Add((float)numericUpDownPitchX.Value);
            }
            else
            {
                int count = row >= col ? row - 1 : col - 1;
                for (int i = 0; i < count; i++)
                {
                    trayPitchY.Add(float.Parse(listViewDistance.Items[i].SubItems[1].Text));
                    trayPitchX.Add(float.Parse(listViewDistance.Items[i].SubItems[2].Text));
                }
            }
            var trayPitch = new {
                same_distance = samePitch,
                pitchX = trayPitchX.ToArray(),
                pitchY = trayPitchY.ToArray()
            };

            //roation
            bool applyRotate = checkBoxRotation.Checked;
            bool rotateByRow = radioButtonRotateRow.Checked;
            bool singleLen = checkBox_SingleRotation.Checked;
            var angles = new List<float>();

            if (applyRotate)
            {
                if (rotateByRow)
                {
                    if(checkBoxRotation.Checked==true)
                    {
                        for (int i = 0; i < row; i++)
                        {
                            angles.Add(float.Parse(listViewRotation.Items[i].SubItems[1].Text));
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < col; i++)
                    {
                        angles.Add(float.Parse(listViewRotation.Items[i].SubItems[1].Text));
                    }
                }
            }
            else
            {
                if (singleLen)
                {
                    for (int i = 0; i < row*col; i++)
                    {
                        if (textBox_SingleAngel.Text=="")
                        {
                            MessageBox.Show("Angle cann't null!Please input a number!","Propmt");
                            return;
                        }
                        angles.Add(float.Parse(textBox_SingleAngel.Text));
                    }
                }
            }
           
            var rotation = new
            {
                apply = applyRotate,
                byRow = rotateByRow,
                single=singleLen,
                angles = angles.ToArray()
            };

            #region name

            bool nameByColumn = radioButtonSameName_column.Checked;
            bool nameByRow = radioButtonSameName_row.Checked;
            bool nameSame = radioButtonSameName_all.Checked;
            var  names = new List<string>();

            if (!nameSame)
            {
                if (rotateByRow)
                {
                    for (int i = 0; i < row; i++)
                    {
                        names.Add(listViewName.Items[i].SubItems[1].Text);
                    }                   
                }
                else
                {
                    for (int i = 0; i < col; i++)
                    {
                        names.Add(listViewName.Items[i].SubItems[1].Text);
                    }
                }
            }
            else
            {
                for (int i = 0; i < row * col; i++)
                {
                    if (textBoxSameName.Text == "")
                    {
                        MessageBox.Show("Name cann't null!Please input a number!", "Propmt");
                        return;
                    }
                    names.Add(textBoxSameName.Text);
                }              
            }

            var NameID = new
            {
                bycolumn = nameByColumn,
                byRow = nameByRow,
                sameName = nameSame,
                names = names.ToArray()
            };
            #endregion


            bool arrayApply = checkBoxMultiArray.Checked;
            bool arrayWorkByRow = radioButtonArrayRow.Checked;
            int arrayRow = (int)numericUpDownArrayRow.Value;
            int arrayCol = (int)numericUpDownArrayCol.Value;
            float arrayPitchX = (float)numericUpDownArrayGapX.Value;
            float arrayPitchY = (float)numericUpDownArrayGapY.Value;
            var arrayInfo = new
            {
                apply = arrayApply,
                rows = arrayRow,
                cols = arrayCol,
                row_first = arrayWorkByRow,
                gap = new
                {
                    x = arrayPitchX,
                    y = arrayPitchY
                }
            };

            bool allValid = checkBoxValidAll.Checked;
            var validInfo = new List<string>();
            if (!allValid)
            {
                for (int i = 0; i < row * arrayRow; i++)
                {
                    StringBuilder trayrow = new StringBuilder();
                    for (int j = 0; j < col * arrayCol; j++)
                    {
                        trayrow.Append(_trayMap[i, j].Activated ? "1" : "0");
                    }

                    validInfo.Add(trayrow.ToString());
                }
            }
            var activationData = new
            {
                all = allValid,
                blocks = validInfo.ToArray()
            };


            using (var file = new FileStream(_fileName, FileMode.Create, FileAccess.Write))
            {
                var sw = new StreamWriter(file);

                var saveObject = new
                {
                    traymap = new
                    {
                        id = id,
                        row = row,
                        col = col,
                        s_loop = sLoop,
                        left_to_right = leftToRight,
                        pitch = trayPitch,
                        rotation = rotation,
                        NameID = NameID,
                        activation = activationData
                    },
                    array = arrayInfo
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

                JObject setting = (JObject)JsonConvert.DeserializeObject(sr.ReadToEnd());

                textBoxTrayName.Text = setting["traymap"]["id"].ToString();

                numericUpDownTrayRow.Value = (int)setting["traymap"]["row"];
                numericUpDownTrayCol.Value = (int)setting["traymap"]["col"];
                checkBoxSLoop.Checked = (bool)setting["traymap"]["s_loop"];
                checkBoxLeftToRight.Checked = (bool)setting["traymap"]["left_to_right"];

                //tray pitch
                checkBoxSamePitch.Checked = (bool)setting["traymap"]["pitch"]["same_distance"];
                var trayPitchX = new List<float>();
                var trayPitchY = new List<float>();
                JArray trayPitch = (JArray)setting["traymap"]["pitch"]["pitchX"];
                for (int i = 0; i < trayPitch.Count; i++)
                {
                    trayPitchX.Add((float)trayPitch[i]);
                }
                trayPitch = (JArray)setting["traymap"]["pitch"]["pitchY"];
                for (int i = 0; i < trayPitch.Count; i++)
                {
                    trayPitchY.Add((float)trayPitch[i]);
                }
                if (checkBoxSamePitch.Checked)
                {
                    numericUpDownPitchY.Value = (decimal)trayPitchX[0];
                    numericUpDownPitchX.Value = (decimal)trayPitchY[0];
                }
                else
                {
                    listViewDistance.Items.Clear();
                    for (int i = 0; i < trayPitchX.Count; i++)
                    {
                        var lvi = new ListViewItem((i + 1).ToString());
                        lvi.SubItems.AddRange(new[] { trayPitchY[i].ToString("F2"), trayPitchX[i].ToString("F2") });

                        listViewDistance.Items.Add(lvi);
                    }
                }

                //roation
                checkBoxRotation.Checked = (bool)setting["traymap"]["rotation"]["apply"];
                radioButtonRotateRow.Checked = (bool)setting["traymap"]["rotation"]["byRow"];
                radioButtonRotateCol.Checked = !radioButtonRotateRow.Checked;
                checkBox_SingleRotation.Checked = (bool)setting["traymap"]["rotation"]["single"];
                JArray rotateAngles = (JArray)setting["traymap"]["rotation"]["angles"];

                if (!checkBox_SingleRotation.Checked)
                {
                    listViewRotation.Items.Clear();
                    for (int i = 0; i < rotateAngles.Count; i++)
                    {
                        var lvi = new ListViewItem((i + 1).ToString());
                        lvi.SubItems.Add((string)rotateAngles[i]);

                        listViewRotation.Items.Add(lvi);
                    }
                }
                else
                {
                    listViewRotation.Items.Clear();
                    if (rotateAngles.Count>0)
                    {
                        textBox_SingleAngel.Text = rotateAngles[0].ToString();
                        for (int i = 0; i < rotateAngles.Count; i++)
                        {
                            var lvi = new ListViewItem((i + 1).ToString());
                            lvi.SubItems.Add((string)rotateAngles[i]);

                            listViewRotation.Items.Add(lvi);
                        }
                    }
                }


                #region name

                radioButtonSameName_column.Checked = (bool)setting["traymap"]["NameID"]["bycolumn"];
                radioButtonSameName_row.Checked = (bool)setting["traymap"]["NameID"]["byRow"];
                radioButtonSameName_all.Checked = (bool)setting["traymap"]["NameID"]["sameName"];
                JArray names = (JArray)setting["traymap"]["NameID"]["names"];

                if (!radioButtonSameName_all.Checked)
                {
                    listViewName.Items.Clear();
                    for (int i = 0; i < names.Count; i++)
                    {
                        var lvi = new ListViewItem((i + 1).ToString());
                        lvi.SubItems.Add((string)names[i]);

                        listViewName.Items.Add(lvi);
                    }

                    //  checkBoxNameEdit.Checked = true;
                    //    listViewName.Visible = true;
                    EnableNameList(true);
                }
                else
                {
                    listViewName.Items.Clear();
                    if (rotateAngles.Count > 0)
                    {
                        textBoxSameName.Text = names[0].ToString();
                    }
                }
                #endregion

                //array setting
                checkBoxMultiArray.Checked = (bool)setting["array"]["apply"];
                radioButtonArrayRow.Checked = (bool)setting["array"]["row_first"];
                radioButtonArrayCol.Checked = !radioButtonArrayRow.Checked;
                numericUpDownArrayRow.Value = (int)setting["array"]["rows"];
                numericUpDownArrayCol.Value = (int)setting["array"]["cols"];
                numericUpDownArrayGapX.Value = (decimal)setting["array"]["gap"]["x"];
                numericUpDownArrayGapY.Value = (decimal)setting["array"]["gap"]["y"];

                //activation
                checkBoxValidAll.Checked = (bool)setting["traymap"]["activation"]["all"];
                var validInfo = (JArray)setting["traymap"]["activation"]["blocks"];

                _rows = -1;
                UpdateSetting();
                if (checkBoxValidAll.Checked)
                {
                    return;
                }

                for (int i = 0; i < validInfo.Count; i++)
                {
                    string data = (string)validInfo[i];
                    for (int j = 0; j < data.Length; j++)
                    {
                        _trayMap[i, j].Activated = data[j] == '1';
                    }
                }
            }
            
        }
    }
}
