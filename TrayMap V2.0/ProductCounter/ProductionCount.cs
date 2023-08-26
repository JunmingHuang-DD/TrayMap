using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace IN3Automation.ProductCounter
{
    /// <summary>
    /// 计算 统计 总产量，UPH,NG总数，良率
    /// </summary>
    [Serializable]
    public class ProductionCount : ISerializable
    {
        int _goodCount, _badCount;
        readonly int _avgCycleCount;
        List<TimeSpan> _avgTime;

        /// <summary>
        /// 计算平均UPH用
        /// </summary>
        private int _totalCounter = 0;

        public int GoodCount
        {
            get { return _goodCount;}
            set {_goodCount = value;}
        }

        public int BadCount
        {
            get { return _badCount;}
            set {_badCount = value;}
        }

        public DateTime LastTime { get; set; }

        public int TotalCount { get { return GoodCount + BadCount; } }

        public double Yield { get { return 100.0 * GoodCount / (GoodCount + BadCount); } }

        /// <summary>
        /// 计算平均UPH，从打开软件开始，计算时间
        /// </summary>
        public double AveragedUPH { get; protected set; }

        public event EventHandler CountChanged;

        public ProductionCount(int averageCC = 4)
        {
            AveragedUPH = 0;
            _avgCycleCount = averageCC;
            _avgTime = new List<TimeSpan>(averageCC);
            ResetCalculateUPH();
        }

        #region save and read data
        /// <summary>
        /// 反序列化，获取数据
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public ProductionCount(SerializationInfo info, StreamingContext context)
        {
            AveragedUPH = 0;
            GoodCount = info.GetInt32("GoodCount");
            BadCount = info.GetInt32("BadCount");
            _avgCycleCount = info.GetInt32("AvgCount");
            _avgTime = new List<TimeSpan>(_avgCycleCount);
            ResetCalculateUPH();
        }

        /// <summary>
        /// 序列化数据到文件
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("GoodCount", GoodCount);
            info.AddValue("BadCount", BadCount);
            info.AddValue("AvgCount", _avgCycleCount);
        }
        #endregion

        public void AddCount(int pass,int fail)
        {
            TimeSpan duration = DateTime.Now - LastTime;

            if (pass > 0)
            {
                GoodCount = _goodCount + pass;
            }

            if (fail > 0)
            {
                BadCount = _badCount + fail;
            }

            _totalCounter += pass;
            _totalCounter += fail;
            AveragedUPH = (_totalCounter / duration.TotalSeconds) * 3600;

            CountChanged?.Invoke(this, null);
        }

        public void Clear()
        {
            _avgTime.Clear();
            ResetCalculateUPH();
            GoodCount = 0;
            BadCount = 0;
            CountChanged?.Invoke(this, null);
        }   
        
        /// <summary>
        /// 重新统计 UPH
        /// </summary>
        public void ResetCalculateUPH()
        {
            LastTime = DateTime.Now;
            _totalCounter = 0;
        }   
    }
}
