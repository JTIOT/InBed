using InBed.Core.Extentions;
using InBed.Service.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Service.Dto
{
    public class SleepingTimeDto
    {
        public SleepingTimeDto()
        {
            objData = new List<SleepData>();
        }
        /// <summary>
        /// 老人id
        /// </summary>
        public int ElderID { get; set; }
        /// <summary>
        /// 老人姓名
        /// </summary>
        public string ElderName { get; set; }
        /// <summary>
        /// 统计日期
        /// </summary>
        public string Date { get; set; }
        /// <summary>
        /// 睡眠数据
        /// </summary>
        public List<SleepData> objData { get; set; }
    }
    public class SleepDto
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public string  BeginTime { get; set; }
       
        /// <summary>
        /// 睡眠类型
        /// </summary>
        public SleepType SleepType { get; set; }
        /// <summary>
        /// 时长(秒)
        /// </summary>
       
        public string SleepTypeName
        {
            get
            {
                return SleepType.ToString();
            }

        }
    }

    public class SleepData
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 睡眠类型
        /// </summary>
        public SleepType SleepType { get; set; }
        /// <summary>
        /// 时长(秒)
        /// </summary>
        public int WhenLong
        {
            get
            {
                return EndTime.Subtract(BeginTime).TotalSeconds.ToString().ToInt();
            }
        }
        public string SleepTypeName
        {
            get
            {
                return SleepType.ToString();
            }

        }
    }
}
