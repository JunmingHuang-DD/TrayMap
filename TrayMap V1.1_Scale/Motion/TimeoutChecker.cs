//========================================================================
// Copyright(C): Incube Automation Co. Ltd
//
// CLR Version : 4.0.30319.18444
// NameSpace : Motion
// FileName : TimeoutChecker
//
// Created by :  at 2016/2/2 16:02:56
//
// Function : 
//
//========================================================================



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Incube.Motion
{
    /// <summary>
    /// 超时检查，倒计时
    /// </summary>
    public class TimeoutChecker
    {
        private DateTime _Timeout;

        /// <summary>
        /// 是否超时判定
        /// </summary>
        public bool IsTimeout 
        {
            get
            {
                return _Timeout < DateTime.Now;
            }
        }

        /// <summary>
        /// 已经运行的时间
        /// </summary>
        public TimeSpan Elapsed
        {
            get
            {
                return _Timeout - DateTime.Now;
            }
        }


        public TimeoutChecker(double seconds) : this(TimeSpan.FromSeconds(seconds))
        {

        }

        public TimeoutChecker(TimeSpan duration)
        {
            _Timeout = DateTime.Now + duration;
        }
    }
}
