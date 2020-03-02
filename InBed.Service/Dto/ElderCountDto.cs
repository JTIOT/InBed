using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InBed.Service.DTO
{
    public class ElderCountDto
    {
        /// <summary>
        /// 系统总人数
        /// </summary>
        public int SumCount { get; set; }
        /// <summary>
        /// 每日新增总人数
        /// </summary>
        public int NewCount { get; set; }
    }
    public class AgeGroupDto
    {
        /// <summary>
        /// 年龄段
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 人数
        /// </summary>
        public int value { get; set; }
    }
    public class GroupElderDto
    {
        /// <summary>
        /// 地区名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 消费人数
        /// </summary>
        public int XFcount { get; set; }
        /// <summary>
        /// 公益人数
        /// </summary>
        public int GYcount { get; set; }
    }
}