using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Entity
{
    public class ElderStatisticsEntity
    {
        public int ElderID { get; set; }
        /// <summary>
        /// 老人姓名
        /// </summary>
        public string ElderName { get; set; }
        /// <summary>
        /// 出生年月
        /// </summary>
        public string Birthday { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string HomePhone { get; set; }
        /// <summary>
        /// 住址
        /// </summary>
        public string HomeAddress { get; set; }
        /// <summary>
        /// 睡眠时长
        /// </summary>
        public string SleepLength { get; set; }
        /// <summary>
        /// 上床时间
        /// </summary>
        public string InBedTime { get; set; }
        /// <summary>
        /// 起夜次数
        /// </summary>
        public int NightCount { get; set; }
        /// <summary>
        /// 深睡时长
        /// </summary>
        public string DeepSleepLength { get; set; }
        /// <summary>
        /// 起床时间
        /// </summary>
        public string GetUpTime { get; set; }
        /// <summary>
        /// 入睡时间
        /// </summary>
        public string FallAsleepTime { get; set; }
        /// <summary>
        /// 平均心率
        /// </summary>
        public string AverageHM { get; set; }
        /// <summary>
        /// 平均呼吸
        /// </summary>
        public string AverageBreath { get; set; }
        /// <summary>
        /// 入睡时长
        /// </summary>
        public string FallAsleepLength { get; set; }

    }
}
