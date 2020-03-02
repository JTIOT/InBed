using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InBed.Service.Request
{
    public class DeviceBindingRequest : BaseRequest
    {
        public string FacilitatorCode { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string DeviceNumber { get; set; }
        /// <summary>
        /// 老人姓名
        /// </summary>
        public string ElderName { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string ElderPhone { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public int DeviceType{ get; set; }
        /// <summary>
        /// 时间查询（开始区间）
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 时间查询（结束区间）
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