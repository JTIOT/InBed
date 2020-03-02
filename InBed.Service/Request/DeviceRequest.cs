using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InBed.Service.Request
{
    public class DeviceRequest:BaseRequest
    {
        /// <summary>
        /// 服务商
        /// </summary>
        public string FacilitatorCode { get; set; }
        /// <summary>
        /// Mac
        /// </summary>
        public string model { get; set; }
        /// <summary>
        /// Mac
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public int DeviceType { get; set; }
        /// <summary>
        /// 设备状态
        /// </summary>
        public int DeviceState { get; set; }
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