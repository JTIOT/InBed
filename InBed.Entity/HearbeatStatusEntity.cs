using System.Collections.Generic;
using InBed.Entity.Base;
using Newtonsoft.Json;
using System;

namespace InBed.Entity
{
    /// <summary>
    /// 心率表
    /// </summary>
    public class HearbeatStatusEntity
    {

        public long id { get; set; }

        public long DeviceID { get; set; }
        /// <summary>
        /// 心率
        /// </summary>
        public int HeartbeatRate { get; set; }
        /// <summary>
        /// 呼吸
        /// </summary>
        public int BreathingRate { get; set; }
        /// <summary>
        /// 重压值
        /// </summary>
        public int PressureValue { get; set; }
        /// <summary>
        /// 是否有心率
        /// </summary>
        public int HaveHR { get; set; }
        /// <summary>
        /// 文件序号
        /// </summary>
        public int FileIndex { get; set; }
        /// <summary>
        /// 设备时间戳
        /// </summary>
        public DateTime TimeStamp { get; set; }
        /// <summary>
        /// 接收时间
        /// </summary>
        public DateTime ReceiveTime { get; set; }
    }
}
