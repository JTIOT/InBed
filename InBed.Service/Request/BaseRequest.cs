using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InBed.Service.Request
{
    public class BaseRequest
    {
        /// <summary>
        /// 接口签名
        /// </summary>
        public string Signature { get; set; }
        /// <summary>
        /// 开始记录数
        /// </summary>
        public int Start { get; set; }
        /// <summary>
        /// 页面长度
        /// </summary>
        public int Length { get; set; }

        
    }
}