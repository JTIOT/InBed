using System.Collections.Generic;
using InBed.Entity.Base;
using Newtonsoft.Json;
using System;

namespace InBed.Entity
{
    /// <summary>
    /// 系统设备表
    /// </summary>
    public class DeviceEntity: BaseEntity
    {
 

        public string Facilitator_Code { get; set; }
        public string Facilitator_Name { get; set; }

        public string device_number { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public int device_type { get; set; }
        /// <summary>
        /// 设备型号
        /// </summary>
        public string device_model { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool is_enable { get; set; }
        /// <summary>
        /// 设备状态
        /// </summary>
        public int status { get; set; }
        


    }
}
