using System.Collections.Generic;
using InBed.Entity.Base;
using Newtonsoft.Json;
using System;

namespace InBed.Entity
{
    /// <summary>
    /// 睡眠类型记录
    /// </summary>
    public class SleepingTimePeriodEntity
    {
        /// <summary>
        /// 睡眠类型记录
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public long DeviceID { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime StopTime { get; set; }
        /// <summary>
        /// 对应心率状态表的ID
        /// </summary>
        public long BeginKeyIndex { get; set; }
        /// <summary>
        /// 对应心率状态表的ID
        /// </summary>
        public long StopKeyIndex { get; set; }
        /// <summary>
        /// 最高心率
        /// </summary>
        public int MaxHB { get; set; }
        /// <summary>
        /// 最低心率
        /// </summary>
        public int MinHB { get; set; }
        /// <summary>
        /// 对应心率状态表的ID
        /// </summary>
        public string MaxHBKeyIndex { get; set; }
        /// <summary>
        /// 对应心率状态表的ID
        /// </summary>
        public string MinHBKeyIndex { get; set; }
        /// <summary>
        /// 最高心率时间
        /// </summary>
        public DateTime MaxHBTime { get; set; }
        /// <summary>
        /// 最低心率时间
        /// </summary>
        public DateTime MinHBTime { get; set; }
        /// <summary>
        /// 睡眠类型
        /// </summary>
        public int SleepType { get; set; }
    }
}
