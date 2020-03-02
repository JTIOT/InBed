using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Service.Request
{
    public class AlarmRequest : BaseRequest
    {
        public string FacilitatorCode { get; set; }
        public string ElderName { get; set; }

        public int  AlarmType { get; set; }
        public string S_data { get; set; }
        public string E_data { get; set; }
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
