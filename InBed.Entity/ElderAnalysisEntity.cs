using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Entity
{
    public class ElderAnalysisEntity
    {
        public int Id { get; set; }
        /// <summary>
        /// 统计日期
        /// </summary>
        public string AnalysisDate { get; set; }
        /// <summary>
        /// 老人id
        /// </summary>
        public int ElderID { get; set; }        
        /// <summary>
        /// 上床时间
        /// </summary>
        public DateTime InBedTime { get; set; }
        /// <summary>
        /// 起床时间
        /// </summary>
        public DateTime GetUpTime { get; set; }
        /// <summary>
        /// 入睡时间
        /// </summary>
        public DateTime FallAsleepTime { get; set; }
        /// <summary>
        /// 清醒时间
        /// </summary>
        public DateTime SoberTime { get; set; }
        /// <summary>
        /// 起夜次数
        /// </summary>
        public int NightCount { get; set; }
        /// <summary>
        /// 睡眠时长
        /// </summary>
        public int SeepLong { get; set; }
        /// <summary>
        /// 浅睡时长
        /// </summary>
        public int DepthSeepLong { get; set; }
        /// <summary>
        /// 深睡时长
        /// </summary>
        public int ShallowSeepLong { get; set; }
        /// <summary>
        /// 清醒时长
        /// </summary>
        public int SoberSeepLong { get; set; }
        /// <summary>
        /// 清醒次数
        /// </summary>
        public int SoberCount { get; set; }
    }
}
