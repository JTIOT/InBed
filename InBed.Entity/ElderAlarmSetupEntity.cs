using System.Collections.Generic;
using InBed.Entity.Base;
using Newtonsoft.Json;
using System;

namespace InBed.Entity
{
    public class ElderAlarmSetupEntity : BaseEntity
    {
        /// <summary>
        /// 老人编号
        /// </summary>
        public long Elder_id { get; set; }
        /// <summary>
        /// 最大心率
        /// </summary>
        public int maxheart { get; set; }
        /// <summary>
        /// 最小心率
        /// </summary>
        public int minheart { get; set; }
        /// <summary>
        /// 夜间最大离床时间(分钟)
        /// </summary>
        public int leavebedtime { get; set; }
        /// <summary>
        /// 最大呼吸次数
        /// </summary>
        public int maxbreath { get; set; }
        /// <summary>
        /// 最小呼吸次数
        /// </summary>
        public int minbreath { get; set; }
        /// <summary>
        /// 呼吸暂停最大时间（分钟）
        /// </summary>
        public int pausebreath { get; set; }
        /// <summary>
        /// 在床最大时间（小时/天)
        /// </summary>
        public int inbedtime { get; set; }
        /// <summary>
        /// 是否人为设定(认为设定|系统设定)
        /// </summary>
        public int set_type { get; set; }
        public string sleeptime { get; set; }
        public string  s_time { get; set; }
        public string e_time { get; set; }
    }
}
