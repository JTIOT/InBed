using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Service.Request
{
   public class RoleRequest:BaseRequest
    {
        public string FacilitatorCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
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
