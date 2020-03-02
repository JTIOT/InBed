using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InBed.Service.Request
{
    public class LogRequest : BaseRequest
    {
        /// <summary>
        /// 登录用户Ip
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 登录用户名
        /// </summary>
        public string LoginName { get; set; }
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