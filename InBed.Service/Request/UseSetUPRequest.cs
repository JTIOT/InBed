using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Service.Request
{
     public class UseSetUPRequest : BaseRequest
    {

        public UseSetUPRequest()
        {
            OrderDir = "desc";
            OrderBy = "id";
        }
        /// <summary>
        /// 使用天数
        /// </summary>
        public int DataCount { get; set; }
        /// <summary>
        /// 服务商
        /// </summary>
        public string FacilitatorCode { get; set; }
        /// <summary>
        /// 老人电话
        /// </summary>
        public string ElderPhone { get; set; }
        /// <summary>
        /// 老人姓名
        /// </summary>
        public string ElderName { get; set; }
        /// <summary>
        /// 用户状态
        /// </summary>
        public int UserState { get; set; }
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
