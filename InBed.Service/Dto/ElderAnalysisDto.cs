using InBed.Core;
using InBed.Core.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Service.Dto
{
    public  class ElderAnalysisDto
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
        /// 老人姓名
        /// </summary>
        public int ElderName { get; set; }
        /// <summary>
        /// 上床时间
        /// </summary>
        public string InBedTime { get; set; }
        /// <summary>
        /// 起床时间
        /// </summary>
        public string GetUpTime { get; set; }
        /// <summary>
        /// 睡眠开始时间
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
        public int SleepLong { get; set; }
        /// <summary>
        /// 浅睡时长
        /// </summary>
        public int DepthSeepLong { get; set; }
        /// <summary>
        /// 深睡时长
        /// </summary>
        public int ShallowSleepLong { get; set; }
        /// <summary>
        /// 清醒时长
        /// </summary>
        public int SoberSleepLong { get; set; }
        /// <summary>
        /// 清醒次数
        /// </summary>
        public int SoberCount { get; set; }

        /// <summary>
        /// 入睡所用时间
        /// </summary>
        public string InSleepTime
        {
            get
            {
                return ClassUtility.MinuteToHour(FallAsleepTime.Subtract(ClassUtility.ToDateTime(InBedTime)).TotalMinutes.ToString().ToInt());
            }

        }
        /// <summary>
        /// 浅睡占总睡眠的比例
        /// </summary>
        public string DepthProportion
        {
            get
            {
                return (DepthSeepLong/ (SleepLong*1.0)*100).ToString()+"%";
            }
        }
        /// <summary>
        /// 深睡占总睡眠的比例
        /// </summary>
        public string ShallowProportion
        {
            get
            {
                return (ShallowSleepLong / (SleepLong * 1.0) * 100).ToString() + "%";
            }
        }
        /// <summary>
        /// 清醒占总睡眠的比例
        /// </summary>
        public string SoberProportion
        {
            get
            {
                return (SoberSleepLong / (SleepLong * 1.0) * 100).ToString() + "%";
            }
        }
    }
}
