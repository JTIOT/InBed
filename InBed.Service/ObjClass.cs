using InBed.Entity;
using InBed.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Service
{
    public class ObjClass
    {
        public static IDictionary<int, MapPositionDto> ElderMap = new Dictionary<int, MapPositionDto>();
        public static IDictionary<int, ElderHeartRate> HeartRateMap = new Dictionary<int, ElderHeartRate>();
        public static IDictionary<int, ElderBR> BRMap = new Dictionary<int, ElderBR>();
        public static IDictionary<int, ElderPressure> PressureMap = new Dictionary<int, ElderPressure>();
        public static Queue<int> HeartRateList = new Queue<int>();
        public static IDictionary<int, ElderHD> ElderHD = new Dictionary<int, ElderHD>();
        /// <summary>
        /// 老人信息
        /// </summary>
        public static IDictionary<int, ElderInfoDto> ElderInfoMap = new Dictionary<int, ElderInfoDto>();

    }
}
