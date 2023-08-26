using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using Incube.Motion;
using System.IO;

namespace CommonUI
{
    public partial class controlTeach : UserControl
    {    
        private TeachSet _TeachSet;//现Teach表

        private int _PreSelectIndex = -1;

        public StringBuilder ModifyLog;//参数修改记录

        /// <summary>
        /// whether the change already saved to disk
        /// </summary>
        public bool ChangeCommitted { get; set; }

        /// <summary>
        /// whether any changes 
        /// </summary>
        [Browsable(false)]
        public bool Modified { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public TeachSet Setting
        {
            get { return _TeachSet; }
            set 
            { 
                _TeachSet = value;

                UpdateTable();
            }
        }

        public event EventHandler OnUpdate;
        public event EventHandler OnSaveUpdate;

        /// <summary>
        /// move request
        /// </summary>
        public event TeachEventHandler MoveClick;


        public controlTeach()
        {
            InitializeComponent();

            InitUI();

            ChangeCommitted = false;
            Modified = false;
            ModifyLog = new StringBuilder();
        }

        private void InitUI()
        {
            gridTeachData.Columns[1].Width = 180;
            gridTeachData.Columns[2].Width = 200;
            gridTeachData.Columns[3].Width = 210;
            gridTeachData.Columns[4].Width = 140;
        }

        private void controlTeach_Load(object sender, EventArgs e)
        {
            flpContainer.Controls.Clear();
            ShowCurrentFineName();
        }

        private void ShowCurrentFineName()
        {
            if (_TeachSet == null)
            {
                ProgramName.Text = "";
            }
            else
            {
                string name = System.IO.Path.GetFileName(_TeachSet.CurrentFileName);
                ProgramName.Text = name;
            }
        }

        public void UpdateTable()
        {
            gridTeachData.Rows.Clear();

            if (_TeachSet == null)
            {
                return;
            }


            foreach (var item in _TeachSet.Items)
            {
                gridTeachData.Rows.Add(
                    item.Category,
                    item.DisplayName,
                    item.Keys,
                    item.Values,
                    item.Updated.ToString("yyyy-MM-dd HH:mm:ss"),
                    item.Description,
                    item.Name);
            }

            gridTeachData.GroupTemplate.Column = ColumnCategory;
            gridTeachData.Sort(ColumnCategory, ListSortDirection.Ascending);
        }

        /// <summary>
        /// display manual set text box for user to input
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="values"></param>
        private void ShowManual(TeachItem item)
        {
            flpContainer.Controls.Clear();

            bool read = !item.IsUserInput;

            labelTitle.Text = item.DisplayName;

            foreach (KeyValue data in item.Data)
            {
                TeachItemControl lbl = new TeachItemControl();
                lbl.Size = new System.Drawing.Size(200, 140);

                lbl.ReadOnly = read;
                lbl.Key = data.Key;
                lbl.LabelName = data.AxisName;
                lbl.TextValue = data.Value;
                lbl.StartSpeed = data.StartSpeed;
                lbl.Speed = data.Speed;
                lbl.Acc = data.Acc;
                lbl.Dec = data.Dec;
                lbl.SmoothTime = data.SmoothTime;

                flpContainer.Controls.Add(lbl);
            }            
        }


        private void dataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0 || _TeachSet == null || _PreSelectIndex == e.RowIndex) return;

            try
            {
                textBoxDes.Text = gridTeachData.Rows[e.RowIndex].Cells[5].Value.ToString();
                textBoxDes.ForeColor = Color.Black;

                string name = gridTeachData.Rows[e.RowIndex].Cells[6].Value.ToString();

                ShowManual(_TeachSet[name]);
            }
            catch (Exception ex)
            {
                textBoxDes.Text = "Error: " + ex.Message;
                textBoxDes.ForeColor = Color.Red;

                _PreSelectIndex = -1;
                return;
            }


            _PreSelectIndex = e.RowIndex;
        }


        /// <summary>
        /// 只更新速度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUpdateSpeed_Click(object sender, EventArgs e)
        {
            if (_PreSelectIndex < 0)
            {
                MessageBox.Show("请先选择位置点");
                return;
            }

            if (DialogResult.OK != MessageBox.Show("确认更新 " + labelTitle.Text + " 示教点速度值", "确认", MessageBoxButtons.OKCancel))
            {
                return;
            }

            string name = gridTeachData.Rows[_PreSelectIndex].Cells[6].Value.ToString();
            ModifyLog.AppendFormat("{0} Change speed (name, speed, acc, dec): ", name);

            for (int i = 0; i < flpContainer.Controls.Count; i++)
            {
                Control ctl = flpContainer.Controls[i];
                if (!(ctl is TeachItemControl))
                {
                    continue;
                }

                TeachItemControl lbl = ctl as TeachItemControl;

                string key = lbl.Key;
                KeyValue setItem = _TeachSet[name, key];

                setItem.StartSpeed = lbl.StartSpeed;
                setItem.Speed = lbl.Speed;
                setItem.Acc = lbl.Acc;
                setItem.Dec = lbl.Dec;
                setItem.SmoothTime = lbl.SmoothTime;

                ModifyLog.AppendFormat("{0}, {1:F1}, {2:F1}, {3:F1}; ", key, setItem.StartSpeed, setItem.Speed, setItem.Acc, setItem.Dec, setItem.SmoothTime);
            }
            ModifyLog.Append("\r\n");

            _TeachSet[name].Updated = DateTime.Now;

            //update grid
            gridTeachData.Rows[_PreSelectIndex].Cells[3].Value = _TeachSet[name].Values;
            gridTeachData.Rows[_PreSelectIndex].Cells[4].Value = _TeachSet[name].Updated.ToString("yyyy-MM-dd HH:mm:ss");


            Modified = true;
            if (OnUpdate != null)
            {
                OnUpdate(_TeachSet[name], new EventArgs());
            }

            //UpdateTable();
            //_TeachSet.SaveTo(_TeachSet.CurrentFileName);
        }

        /// <summary>
        /// update position and speed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (_PreSelectIndex < 0)
            {
                MessageBox.Show("请先选择位置点");
                return;
            }

            if (DialogResult.OK != MessageBox.Show("确认更新 " + labelTitle.Text + " 示教点的速度值和位置值", "确认", MessageBoxButtons.OKCancel))
            {
                return;
            }


            string name = gridTeachData.Rows[_PreSelectIndex].Cells[6].Value.ToString();
            ModifyLog.AppendFormat("{0} Change speed and position (name, pos, speed, acc, dec): ", name);

            for (int i = 0; i < flpContainer.Controls.Count; i++)
            {
                Control ctl = flpContainer.Controls[i];
                if (!(ctl is TeachItemControl))
                {
                    continue;
                }


                TeachItemControl lbl = ctl as TeachItemControl;

                string key = lbl.Key;
                KeyValue setItem = _TeachSet[name, key];

                if (_TeachSet[name].IsUserInput)
                {
                    //read user set value
                    setItem.Value = lbl.TextValue;
                }
                else
                {
                    //read current axis position  读取当前轴的位置
                    if (MotionFactory.Instance.Axes.ContainsKey(key))
                    {
                        setItem.Value = MotionFactory.Instance.Axes[key].Position;
                    }                    
                }

                setItem.StartSpeed = lbl.StartSpeed;
                setItem.Speed = lbl.Speed;
                setItem.Acc = lbl.Acc;
                setItem.Dec = lbl.Dec;
                setItem.SmoothTime = lbl.SmoothTime;

                Modified = true;
                ModifyLog.AppendFormat("{0}, {1:F3}, {2:F1}, {3:F1}, {4:F1}; ", key, setItem.Value, setItem.StartSpeed, setItem.Speed, setItem.Acc, setItem.Dec, setItem.SmoothTime);
            }

            ModifyLog.Append("\r\n");

            _TeachSet[name].Updated = DateTime.Now;

            //update grid
            gridTeachData.Rows[_PreSelectIndex].Cells[3].Value = _TeachSet[name].Values;
            gridTeachData.Rows[_PreSelectIndex].Cells[4].Value = _TeachSet[name].Updated.ToString("yyyy-MM-dd HH:mm:ss");

            if (OnUpdate != null)
	        {
                OnUpdate(_TeachSet[name], new EventArgs());
	        }

            //UpdateTable();
            //_TeachSet.SaveTo(_TeachSet.CurrentFileName);
        }

        private void glassButtonMove_Click(object sender, EventArgs e)
        { 
            if (_PreSelectIndex < 0)
            {
                MessageBox.Show("请先选择位置点");
                return;
            }

            //if (DialogResult.OK != MessageBox.Show("Are you sure to move ?", "Confirm", MessageBoxButtons.OKCancel))
            if (DialogResult.OK != MessageBox.Show("确认移动到 " + labelTitle.Text + " 位置 ", "确认", MessageBoxButtons.OKCancel))
            {
                return;
            }

            //move to destination
            if (MoveClick != null)
            {
                string name = gridTeachData.Rows[_PreSelectIndex].Cells[6].Value.ToString();

                MoveClick(this, new TeachEventArgs(_TeachSet[name]));
            }
        }
        
        private void buttonSaveUpdate_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK != MessageBox.Show("确认保存更新? ", "确认", MessageBoxButtons.OKCancel))
            {
                return;
            }
            //log the chnage logs
            //for (int i = 0; i < _MC_Teach.Items.Count; i++)
            //{
            //    CompareTeachItem(_MC_Teach.Items[i], _TeachSet.Items[i]);
            //}

            if (OnSaveUpdate != null)
            {
                OnSaveUpdate(this, new EventArgs());
            }
            
            string _Path = System.AppDomain.CurrentDomain.BaseDirectory;
         //   Path.Combine(_Path, "Setting", "Products", , "TeachData.xml");
           _TeachSet.SaveTo(_TeachSet.CurrentFileName);

            ChangeCommitted = true;
            Modified = false;

            MessageBox.Show("保存更新成功");
        }

     

        private void ProgramLoad_Click(object sender, EventArgs e)
        {
            string InitialDirectory = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "\\Setting");
            string configFileName = "";
            string teachFileName = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = InitialDirectory;
            openFileDialog.Filter = "参数文件(*.xml)|*.xml";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                configFileName = openFileDialog.FileName;

                string path = System.IO.Path.GetDirectoryName(configFileName);
                string name = System.IO.Path.GetFileName(configFileName);
                int iIndex = name.LastIndexOf('.');
                name = name.Substring(0, iIndex);
                teachFileName = System.IO.Path.Combine(path, name + ".xml");
            }
            else
            {
                return;
            }

            try
            {
                if (System.IO.File.Exists(configFileName) == false)
                {
                    MessageBox.Show("装载新程序参数失败：不存在路径为" + configFileName + "的配置文件");
                    return;
                }
                if (System.IO.File.Exists(teachFileName) == false)
                {
                    MessageBox.Show("装载新程序参数失败：不存在路径为" + teachFileName + "的配置文件");
                    return;
                }

                _TeachSet = TeachSet.LoadFrom(configFileName);
                ShowCurrentFineName();

                //if (_MC.ReLoadSetting(configFileName, teachFileName))
                //{
                //    _Config = new Config(_MC.Settings);
                //    propertyGrid1.SelectedObject = _Config;
                //    ShowCurrentFineName();
                //    MessageBox.Show("装载新程序参数成功");
                //}
                //else
                //{
                //    MessageBox.Show("装载新程序参数失败");
                //}


            }
            catch (Exception ex)
            {
                MessageBox.Show("装载新程序参数失败:" + ex.Message.ToString());
            }
        }

        private void ProgramSaveAs_Click(object sender, EventArgs e)
        {
            string InitialDirectory = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Setting");
            string configFileName = "";
            string teachFileName = "";
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "参数文件(*.xml)|*.xml";
            saveFileDialog.DefaultExt = "*.xml";
            saveFileDialog.InitialDirectory = InitialDirectory;
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.AddExtension = true;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                configFileName = saveFileDialog.FileName;

                string path = System.IO.Path.GetDirectoryName(configFileName);
                string name = System.IO.Path.GetFileName(configFileName);
                int iIndex = name.LastIndexOf('.');
                name = name.Substring(0, iIndex);
                teachFileName = System.IO.Path.Combine(path, name + ".xml");

            }
            else
            {
                return;
            }

            try
            {
                _TeachSet.SaveTo(configFileName); 
                MessageBox.Show("程序参数另存为成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("程序参数另存为失败:" + ex.Message.ToString());
            }
        }
    }

    public delegate void TeachEventHandler(object sender, TeachEventArgs e);

    public class TeachEventArgs : EventArgs
    {
        private TeachItem _Data;

        public TeachItem Data
        {
            get { return _Data; }
            set { _Data = value; }
        }

        public TeachEventArgs(TeachItem data)
        {
            Data = data;
        }
    }
}
