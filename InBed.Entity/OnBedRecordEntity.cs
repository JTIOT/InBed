using System.Collections.Generic;
using InBed.Entity.Base;
using Newtonsoft.Json;
using System;

namespace InBed.Entity
{
    /// <summary>
    /// 上下床记录
    /// </summary>
    public class OnBedRecordEntity
    {
        public long id { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public long DeviceID { get; set; }
        /// <summary>
        /// 上床时间
        /// </summary>
        public DateTime OnBedTime { get; set; }
        /// <summary>
        /// 离床时间
        /// </summary>
        public DateTime LeaveBedTime { get; set; }
        /// <summary>
        /// 对应心率状态表的ID
        /// </summary>
        public long OnBedKeyIndex { get; set; }
        /// <summary>
        /// 对应心率状态表的ID
        /// </summary>
        public long LeaveBedKeyIndex { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 上床判断时间
        /// </summary>
        public string OnBedDetectTime { get; set; }
        /// <summary>
        /// 离床判断时间
        /// </summary>
        public string LeaveBedDetectTime { get; set; }
    }
}
