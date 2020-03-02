using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Service.Request
{
    public class AlarmSetupRequest: BaseRequest
    {
        public string FacilitatorCode { get; set; }
        public string ElderName { get; set; }
        public string ElderPhone { get; set; }
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
