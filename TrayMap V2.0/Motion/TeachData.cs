// ===============================================================================
// Project Name        :    CommonUI
// Project Description :    
// ===============================================================================
// Class Name          :    TeachData
// Class Version       :    v1.0.0.0
// Class Description   :    
// Author              :    Administrator
// Create Time         :    2014/10/11 10:14:26
// Update Time         :    2014/10/11 10:14:26
// ===============================================================================
// Copyright © IN3 2014 . All rights reserved.
// ===============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.IO;

namespace Incube.Motion
{
    /// <summary>
    /// a class represent a teach pos setting for a specified axis
    /// </summary>
    [XmlType("KeyValue")]
    public class KeyValue : ICloneable
    {
        [XmlAttribute("Key")]
        public string Key { get; set; }

        [XmlAttribute("Value")]
        public double Value { get; set; }

        [XmlAttribute("StartSpeed")]
        public double StartSpeed { get; set; }

        [XmlAttribute("Speed")]
        public double Speed { get; set; }

        [XmlAttribute("Acc")]
        public double Acc { get; set; }

        [XmlAttribute("Dec")]
        public double Dec { get; set; }

        [XmlAttribute("SmoothTime")]
        public double SmoothTime { get; set; }

        [XmlIgnore]
        public string AxisName
        {
            get
            {
                if (MotionFactory.Instance.Axes.ContainsKey(Key))
                {
                    return MotionFactory.Instance.Axes[Key].Setting.DisplayName;
                }
                else
                {
                    return Key;
                }
            }
        }

        public KeyValue()
        {

        }

        public KeyValue(string key, double v)
        {
            Key = key;
            Value = v;

            StartSpeed = 10;
            Speed = 100;
            Acc = Dec = 1000;
            SmoothTime = 0.1;
        }

        public KeyValue(string key, double v, double startSpeed,double speed, double acc, double dec,double smoothTime)
        {
            Key = key;
            Value = v;

            StartSpeed = startSpeed;
            Speed = speed;
            Acc = acc;
            Dec = dec;
            SmoothTime = smoothTime;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    /// <summary>
    /// teach pos
    /// </summary>
    [XmlType("TeachItem")]
    [XmlInclude(typeof(KeyValue))]

    public class TeachItem : ICloneable
    {
        private List<KeyValue> _Data;
        private DateTime _Updated;

        [XmlElement("Name")]
        public string Name { get; set; }


        [XmlElement("Display")]
        public string DisplayName { get; set; }

        [XmlElement("DisplayEnglish")]
        public string DisplayNameEnglish { get; set; }

        /// <summary>
        /// 分组信息
        /// </summary>
        [XmlElement("Group")]
        public int Group { get; set; }


        [XmlElement("Updated")]
        public DateTime Updated
        {
            get { return _Updated; }
            set { _Updated = value; }
        }

        /// <summary>
        /// 每个组中可能的分类信息
        /// </summary>
        [XmlElement("Category")]
        public string Category { get; set; }

        [XmlElement("CategoryEnglish")]
        public string CategoryEnglish { get; set; }

        [XmlElement("Description")]
        public string Description { get; set; }

        [XmlElement("DescriptionEnglish")]
        public string DescriptionEnglish { get; set; }

        [XmlIgnore()]
        public string Keys
        {
            get
            {
                string keys = "";
                foreach (var key in _Data.Select(e => e.AxisName))
                {
                    keys += key + ",";
                }

                return keys;
            }
        }

        [XmlIgnore()]
        public string Values
        {
            get
            {
                string values = "";
                foreach (var v in _Data.Select(e => e.Value))
                {
                    values += v.ToString("F3") + ",";
                }

                return values;
            }
        }

        [XmlIgnore()]
        public KeyValue this[string key]
        {
            get
            {
                foreach (var item in _Data)
                {
                    if (item.Key == key)
                    {
                        return item;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// whether it is set with input value, such as offset. By default, it need to read data 
        /// </summary>
        [XmlElement("IsUserInput")]
        public bool IsUserInput { get; set; }


        [XmlArray("Data")]
        [XmlArrayItem("DataItem")]
        public List<KeyValue> Data
        {
            get { return _Data; }
            set { _Data = value; }
        }

        public TeachItem()
        {
            _Data = new List<KeyValue>();
            _Updated = DateTime.Now;
            Category = "General";
            Description = "";
            DisplayName = "";
            DescriptionEnglish = "";
            DisplayNameEnglish = "";
        }


    
        public object Clone()
        {
 	        TeachItem ti = new TeachItem();
            ti._Data = new List<KeyValue>(_Data.Count);

            foreach (var item in _Data)
	        {
                ti._Data.Add((KeyValue)item.Clone());
	        }

            ti._Updated = this._Updated;
            ti.Category = this.Category;
            ti.CategoryEnglish = this.CategoryEnglish;
            ti.Group = this.Group;
            ti.Description = this.Description;
            ti.DescriptionEnglish = this.DescriptionEnglish;
            ti.Name = this.Name;
            ti.IsUserInput = this.IsUserInput;
            ti.DisplayName = this.DisplayName;
            ti.DisplayNameEnglish = this.DisplayNameEnglish;

            return ti;
        }
    }

    /// <summary>
    /// Teaching data collection
    /// </summary>
    [XmlRoot("TeachingSets")]
    [XmlInclude(typeof(TeachItem)), XmlInclude(typeof(KeyValue))]
    public class TeachSet : ICloneable
    {

        private List<TeachItem> _Items = new List<TeachItem>();

        [XmlArray(ElementName = "TeachItems")]
        [XmlArrayItem(ElementName = "Item", Type = typeof(TeachItem), IsNullable = false)]
        public List<TeachItem> Items
        {
            get { return _Items; }
            set
            {
                _Items = value;
            }
        }

        [XmlIgnore]
        public TeachItem this[string name]
        {
            get
            {
                foreach (var item in _Items)
                {
                    if (item.Name == name)
                    {
                        return item;
                    }
                }

                return null;
            }
        }

        [XmlIgnore]
        public KeyValue this[string name, string key]
        {
            get
            {
                foreach (var item in _Items)
                {
                    if (item.Name == name)
                    {
                        return item[key];
                    }
                }

                return null;
            }

            set
            {
                foreach (var item in _Items)
                {
                    if (item.Name == name)
                    {
                        for (int i = 0; i < item.Data.Count; i++ )
                        {
                            if (item.Data[i].Key == key)
                            {
                                item.Data[i] = value; 
                            }
                        }
                    }
                }
            }
        }

        [XmlIgnore]
        public string CurrentFileName { get; set; }

    
        public TeachSet()
        {
        }

        public TeachSet(TeachSet copy)
        {
            _Items.Clear();
            foreach (var item in copy.Items)
            {
                _Items.Add(item);
            }
        }


        /// <summary>
        /// save current setting to a XML file
        /// </summary>
        /// <param name="file">file path</param>
        public void SaveTo(string file)
        {
            //backup file if exist
            if (File.Exists(file))
            {
                string path = Path.GetDirectoryName(file);
                string name = Path.GetFileName(file);
                path = Path.Combine(path, "backup");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                path = Path.Combine(path, name + DateTime.Now.ToString(".yyyyMMddHHmmssfff") + ".bak");

                File.Move(file, path);
            }

            using (FileStream fs = new FileStream(file, FileMode.Create))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(TeachSet),
                                            new Type[] { typeof(TeachItem), typeof(KeyValue) });

                formatter.Serialize(fs, this);
            }
        }

        /// <summary>
        /// deserialize the setting object from a xml file
        /// </summary>
        /// <param name="file">file path</param>
        /// <returns></returns>
        public static TeachSet LoadFrom(string file)
        {
            TeachSet t = null;

            using (FileStream fs = new FileStream(file, FileMode.Open))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(TeachSet),
                    new Type[] { typeof(TeachItem), typeof(KeyValue) });

                t = (TeachSet)formatter.Deserialize(fs);

                t.CurrentFileName = file;
            }

            return t;
        }

        public object Clone()
        {
            TeachSet ts = new TeachSet();
            ts._Items = new List<TeachItem>(this._Items.Count);
            foreach (var item in _Items)
            {
                ts._Items.Add((TeachItem)item.Clone());
            }

            ts.CurrentFileName = CurrentFileName;

            return ts;
        }
    }


 }
