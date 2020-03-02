using System.Collections.Generic;
using InBed.Entity.Base;
using Newtonsoft.Json;
using System;

namespace InBed.Entity
{
    public class SystemSetupEntity : BaseEntity
    {
        public SystemSetupEntity()
        {
            user = new UserEntity();

        }
        public int is_head { get; set; }
        /// <summary>
        /// 头图片地址
        /// </summary>
        public string headpicture_url { get; set; }
        /// <summary>
        /// 都文字
        /// </summary>
        public string head_value { get; set; }
        /// <summary>
        /// 是否显示菜单
        /// </summary>
        public string is_menu { get; set; }
        /// <summary>
        /// 显示默认主页
        /// </summary>
        public string index_url { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public UserEntity user { get; set; }
    }

    public class WeekSleepingEntity
    {

        public int ProductID { get; set; }
        public string BeginTime { get; set; }
        public int SleepType { get; set; }
        public string  Data { get; set; }

    }
    public class DaySleepingEntity
    {

        public int ProductID { get; set; }
        public string BeginTime { get; set; }
        public string StopTime { get; set; }
        public int SleepType { get; set; }
        public string Data { get; set; }

    }
}
