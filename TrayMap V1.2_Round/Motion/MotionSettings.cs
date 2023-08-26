using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Incube.Motion
{
    /// <summary>
    /// 本机使用的控制卡信息，保存到app.config文件中
    /// </summary>
    public class CardElement : ConfigurationElement
    {
        [ConfigurationProperty("typeName", IsKey = true, IsRequired = true)]
        public string TypeName
        {
            get
            {
                return (string)this["typeName"];
            }
            set
            {
                this["typeName"] = value;
            }
        }


        [ConfigurationProperty("nameSpace", IsKey = false, IsRequired = false)]
        public string CardNameSpace
        {
            get
            {
                return (string)this["nameSpace"];
            }
            set
            {
                this["nameSpace"] = value;
            }
        }

        [ConfigurationProperty("assembly", DefaultValue = "", IsRequired = false)]
        public string Assembly
        {
            get
            {
                return (string)this["assembly"];
            }
            set
            {
                this["assembly"] = value;
            }
        }
    }

    /// <summary>
    /// 多个控制卡信息
    /// </summary>
    public class CardCollection : ConfigurationElementCollection
    {
        public CardElement this[int index]
        {
            get
            {
                return base.BaseGet(index) as CardElement;
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
            return new CardElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CardElement)element).TypeName;
        }
    }

    /// <summary>
    /// section区块
    /// </summary>
    public class CardSection : ConfigurationSection
    {
        [ConfigurationProperty("motionCards", IsDefaultCollection = false),
         ConfigurationCollection(typeof(CardCollection), AddItemName = "card")]
        public CardCollection MotionCards
        {
            get
            {
                return (CardCollection)this["motionCards"] ??
                   new CardCollection();
            }
        }
    }

}
