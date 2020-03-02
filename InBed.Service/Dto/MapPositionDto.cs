using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Service.Dto
{
     public class MapPositionDto
    {
        public int Id { get; set; }
        /// <summary>
        ///家庭地址
        /// </summary>
        public string Homeaddress { get; set; }
        /// <summary>
        ///地图坐标
        /// </summary>
        public string MapPoint { get; set; }
        /// <summary>
        /// 是否在床
        /// </summary>
        public int IsBed { get; set; }
        /// <summary>
        /// 最后一次获取时间
        /// </summary>
        public DateTime NewTime { get; set; }
        /// <summary>
        /// 是否重画
        /// </summary>
        public int Flag { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDel { get; set; }

        /// <summary>
        ///服务商名字
        /// </summary>
        public string FacilitatorName { get; set; }

    }
}
