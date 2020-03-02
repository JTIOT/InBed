using InBed.Service.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InBed.Service.Request
{
    public class MenuRequest : BaseRequest
    {
        /// <summary>
        /// 上级菜单编号
        /// </summary>
        public int P_id { get; set; }
        /// <summary>
        /// 菜单类型
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 菜单名
        /// </summary>
        public string Name { get; set; }
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