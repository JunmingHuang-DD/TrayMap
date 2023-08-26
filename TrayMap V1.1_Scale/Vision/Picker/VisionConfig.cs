using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

namespace Incube.Vision.Picker
{
    [Serializable]
    public class VisionConfig : ICloneable
    {
        [Category("LeftPicker"),LocalizedDisplayName("DisplayVisionSettingsL"), LocalizedDescription("DescripVisionSettingsL")]
        [XmlElement("左吸嘴")]      
        public vpPickerParam LeftPicker { get; set; }

        [Category("RightPicker"), LocalizedDisplayName("DisplayVisionSettingsR"), LocalizedDescription("DescripVisionSettingsR")]
        [XmlElement("右吸嘴")]
        public vpPickerParam RightPicker { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public string CurrentFileName { get; set; }

        public VisionConfig()
        {
            CurrentFileName = "./Setting/VisionSettings.xml";

            #region LeftPicker
            LeftPicker = new vpPickerParam()
            {
                Name = "LeftVisionPicker",
                Pick = new pVison()
                {
                    Vpos = new vTeachPostion()
                    {
                        TeachPos = new Point3D(0, 0, 0),
                        TeachPixel = new Point3D(0, 0, 0)
                    },
                    ThisMpp = new MPP()
                    {
                        X_MPP = 0,
                        Y_MPP = 0
                    },
                    Direction = new VisionDirection()
                    {
                        X_Direction = true,
                        Y_Direction = true,
                        R_Direction = true
                    },
                    CameraAngel = 0,
                    ClockwiseIsVisionP = true
                },
                Place_UpVision = new UpVision()
                {
                    RotateCenter = new System.Drawing.PointF(0,0),
                    Vpos = new vTeachPostion()
                    {
                        TeachPos = new Point3D(0, 0, 0),
                        TeachPixel = new Point3D(0, 0, 0)
                    },
                    ThisMpp = new MPP()
                    {
                        X_MPP = 0,
                        Y_MPP = 0
                    },
                    Direction = new VisionDirection()
                    {
                        X_Direction = true,
                        Y_Direction = true,
                        R_Direction = true
                    },
                    CameraAngel = 0,
                    ClockwiseIsVisionP = true
                },
                Place_DnVision = new pVison()
                {
                    Vpos = new vTeachPostion()
                    {
                        TeachPos = new Point3D(0, 0, 0),
                        TeachPixel = new Point3D(0, 0, 0)
                    },
                    ThisMpp = new MPP()
                    {
                        X_MPP = 0,
                        Y_MPP = 0
                    },
                    Direction = new VisionDirection()
                    {
                        X_Direction = true,
                        Y_Direction = true,
                        R_Direction = true
                    },
                    CameraAngel = 0,
                    ClockwiseIsVisionP = true
                }            
            };
            #endregion

            #region RightPicker
            RightPicker = new vpPickerParam()
            {
                Name = "RightVisionPicker",
                Pick = new pVison()
                {
                    Vpos = new vTeachPostion()
                    {
                        TeachPos = new Point3D(0, 0, 0),
                        TeachPixel = new Point3D(0, 0, 0)
                    },
                    ThisMpp = new MPP()
                    {
                        X_MPP = 0,
                        Y_MPP = 0
                    },
                    Direction = new VisionDirection()
                    {
                        X_Direction = true,
                        Y_Direction = true,
                        R_Direction = true
                    },
                    CameraAngel = 0,
                    ClockwiseIsVisionP = true
                },
                Place_UpVision = new UpVision()
                {
                    RotateCenter = new System.Drawing.PointF(0, 0),
                    Vpos = new vTeachPostion()
                    {
                        TeachPos = new Point3D(0, 0, 0),
                        TeachPixel = new Point3D(0, 0, 0)
                    },
                    ThisMpp = new MPP()
                    {
                        X_MPP = 0,
                        Y_MPP = 0
                    },
                    Direction = new VisionDirection()
                    {
                        X_Direction = true,
                        Y_Direction = true,
                        R_Direction = true
                    },
                    CameraAngel = 0,
                    ClockwiseIsVisionP = true
                },
                Place_DnVision = new pVison()
                {
                    Vpos = new vTeachPostion()
                    {
                        TeachPos = new Point3D(0, 0, 0),
                        TeachPixel = new Point3D(0, 0, 0)
                    },
                    ThisMpp = new MPP()
                    {
                        X_MPP = 0,
                        Y_MPP = 0
                    },
                    Direction = new VisionDirection()
                    {
                        X_Direction = true,
                        Y_Direction = true,
                        R_Direction = true
                    },
                    CameraAngel = 0,
                    ClockwiseIsVisionP = true
                }
            };
            #endregion
        }
        public object Clone()//克隆
        {
            return DeepCopyByBin<VisionConfig>(this);
        }

        public static T DeepCopyByBin<T>(T obj)
        {
            object retval;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                //序列化成流
                bf.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                //反序列化成对象
                retval = bf.Deserialize(ms);
                ms.Close();
            }
            return (T)retval;
        }
        public VisionConfig Load(string fileName)
        {
            VisionConfig t = null;
            CurrentFileName = fileName;

            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                System.Xml.Serialization.XmlSerializer formatter =
                    new System.Xml.Serialization.XmlSerializer(typeof(VisionConfig),
                                new Type[] {
                                    typeof(vpPickerParam),
                                    typeof(vpPickerParam)
                                });

                t = (VisionConfig)formatter.Deserialize(fs);
                t.CurrentFileName = fileName;
            }
            return t;
        }

        public void Save(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                System.Xml.Serialization.XmlSerializer formatter = new System.Xml.Serialization.XmlSerializer(
                    typeof(VisionConfig),
                                new Type[] {
                                    typeof(vpPickerParam),
                                    typeof(vpPickerParam)
                                });

                formatter.Serialize(fs, this);
            }
        }
    }
}
