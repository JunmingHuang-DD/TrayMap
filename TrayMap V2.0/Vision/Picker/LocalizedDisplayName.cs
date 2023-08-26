using Incube.Vision.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incube.Vision.Picker
{
        public class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {
        private readonly string resourceName;
        public LocalizedDisplayNameAttribute(string resourceName)
            : base()
        {
            this.resourceName = resourceName;
        }

        public override string DisplayName
        {
            get
            {
                string filePath = System.AppDomain.CurrentDomain.BaseDirectory + @"\Setting\language" + @"\Language.txt";

                StreamReader objReader = new StreamReader(filePath);
                string language = objReader.ReadLine();
                objReader.Close();

                if (language.Contains("English"))
                {
                    return Resources_en.ResourceManager.GetString(this.resourceName);
                }
                else if (language.Contains("Chinese"))
                {
                    return Resources.ResourceManager.GetString(this.resourceName);
                }
                else
                {
                    return "Unknown linguistic environment!";
                }
            }
        }
    }

    public class LocalizedDescriptionAttribute : DescriptionAttribute
    {
        private readonly string resourceName;
        public LocalizedDescriptionAttribute(string resourceName)
            : base()
        {
            this.resourceName = resourceName;
        }

        public override string Description
        {
            get
            {
                string filePath = System.AppDomain.CurrentDomain.BaseDirectory + @"\Setting\language" + @"\Language.txt";

                StreamReader objReader = new StreamReader(filePath);
                string language = objReader.ReadLine();
                objReader.Close();

                if (language.Contains("English"))
                {
                    return Resources_en.ResourceManager.GetString(this.resourceName);
                }
                else if (language.Contains("Chinese"))
                {
                    return Resources.ResourceManager.GetString(this.resourceName);
                }
                else
                {
                    return "Unknown linguistic environment!";
                }
            }
        }
    }
}
