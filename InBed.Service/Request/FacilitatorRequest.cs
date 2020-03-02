using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InBed.Service.Request
{
    public class FacilitatorRequest : BaseRequest
    {
        /// <summary>
        /// 服务商
        /// </summary>
        public string FacilitatorCode { get; set; }
        /// <summary>
        /// 服务商名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 工商注册码
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 上级服务商
        /// </summary>
        public int UP_id { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Adders { get; set; }
        /// <summary>
        /// 创建用户时间查询（开始区间）
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 创建用户时间查询（结束区间）
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string OrderBy { get; set; }
        /// <summary>
        /// 排序方式
        /// </summary>
        public string OrderDir { get; set; }
    }
}