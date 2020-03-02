using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Service.Dto
{
   public class ProvinceNumberDto
    {
        /// <summary>
        /// 地名
        /// </summary>
        public string PlaceNamee { get; set; }
        /// <summary>
        /// 人数
        /// </summary>
        public int Number { get; set; }
    }
    public class ProvinceOnlineNumberDto
    {
        /// <summary>
        /// 地名
        /// </summary>
        public string PlaceNamee { get; set; }
        /// <summary>
        /// 总人数
        /// </summary>
        public int SumNumber { get; set; }
        /// <summary>
        /// 在线人数
        /// </summary>
        public int OnlineNumber { get; set; }
    }
}
