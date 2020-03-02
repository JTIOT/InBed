using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Entity
{
   public class ElderCountEntity
    {
        /// <summary>
        /// 系统总人数
        /// </summary>
        public int SumCount { get; set; }
        /// <summary>
        /// 每日新增总人数
        /// </summary>
        public int NewCount { get; set; }
    }
    public class ProvinceNumberEntity
    {
        /// <summary>
        /// 省份
        /// </summary>
        public string PlaceNamee { get; set; }
        /// <summary>
        /// 人数
        /// </summary>
        public int Number { get; set; }
    }

    public class AgeGroupEntity
    {
        /// <summary>
        /// 年龄段
        /// </summary>
        public string Age { get; set; }
        /// <summary>
        /// 人数
        /// </summary>
        public int Number { get; set; }
    }
}
