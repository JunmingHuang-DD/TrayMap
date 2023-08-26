using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Incube.Vision
{
    /// <summary>
    /// 相机参数设置，保存到app.config文件中
    /// </summary>
    public class CameraElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("id", DefaultValue = "", IsRequired = true)]
        public string ID
        {
            get
            {
                return (string)this["id"];
            }
            set
            {
                this["id"] = value;
            }
        }

        [ConfigurationProperty("mpp", IsRequired = true)]
        public double MPP
        {
            get
            {
                return (double)this["mpp"];
            }
            set
            {
                this["mpp"] = value;
            }
        }

        [ConfigurationProperty("exposure", IsRequired = true)]
        public double Exposure
        {
            get
            {
                return (double)this["exposure"];
            }
            set
            {
                this["exposure"] = value;
            }
        }
    }

    /// <summary>
    /// 多个相机设置
    /// </summary>
    public class CameraCollection : ConfigurationElementCollection
    {
        public CameraElement this[int index]
        {
            get
            {
                return base.BaseGet(index) as CameraElement;
            }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }

                this.BaseAdd(index, value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new CameraElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CameraElement)element).Name;
        }
    }

    public class CameraSection : ConfigurationSection
    {
        [ConfigurationProperty("cameras", IsDefaultCollection = false),
         ConfigurationCollection(typeof(CameraCollection), AddItemName = "camera")]
        public CameraCollection Cameras
        {
            get
            {
                return (CameraCollection)this["cameras"] ??
                   new CameraCollection();
            }
        }
    }

}
