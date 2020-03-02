using System.Collections.Generic;
using InBed.Entity.Base;
using Newtonsoft.Json;
using System;

namespace InBed.Entity
{
    /// <summary>
    /// 服务商信息
    /// </summary>
    public class FacilitatorEntity: BaseEntity
    {

        public string code { get; set; }
        /// <summary>
        /// 服务商名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 区县
        /// </summary>
        public string district { get; set; }
        /// <summary>
        /// 工商注册码
        /// </summary>
        public string number { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string contacts { get; set; }
        /// <summary>
        /// 详细注册地址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 注册资本
        /// </summary>
        public int capital { get; set; }
        /// <summary>
        /// 营业范围
        /// </summary>
        public string management { get; set; }
        /// <summary>
        /// 上级服务商
        /// </summary>
        public int? up_id { get; set; }
        /// <summary>
        /// 服务商等级
        /// </summary>
        public int F_Level { get; set; }
        
    }
}
