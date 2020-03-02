using System.Collections.Generic;
using InBed.Entity.Base;
using Newtonsoft.Json;
using System;

namespace InBed.Entity
{
    /// <summary>
    /// 系统设置老人使用有效期
    /// </summary>
    public class ElderSetupEntity : BaseEntity
    {
        public string Facilitator_Code { get; set; }
        public string Facilitator_Name { get; set; }
        /// <summary>
        /// 老人编号
        /// </summary>
        public long Elder_id { get; set; }
        /// <summary>
        /// 开始使用时间
        /// </summary>
        public DateTime start_data { get; set; }
        /// <summary>
        /// 结束使用时间
        /// </summary>
        public DateTime end_data { get; set; }
        /// <summary>
        /// 是否停用
        /// </summary>
        public int is_enable { get; set; }

      

    }
}
