using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Service.Request
{
    public class ElderAnalysisRequest : BaseRequest
    {
        /// <summary>
        /// 服务商
        /// </summary>
        public string FacilitatorCode { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string ElderName { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public int ElderID { get; set; }
        public string StartTime { get; set; }
        /// <summary>
        /// 创建用户时间查询（结束区间）
        /// </summary>
        public string EndTime { get; set; }
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
