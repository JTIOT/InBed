using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Service.Dto
{
    public class HeartRateDto
    {
        public AlarmSetupDto ElderSetUP { get; set; }
        public List<HeartRateData> Data { get; set; }
        public List<BreathingRate> BRData { get; set; }
        public List<Pressure> PressureData { get; set; }
        public HeartRateDto()
        {
            ElderSetUP = new AlarmSetupDto();
            Data = new List<HeartRateData>();
            BRData = new List<BreathingRate>();
            PressureData = new List<Pressure>(); 
        }
    }
    public class BreathingRate
        {
        public int id { get; set; }
        public string value { get; set; }
        public string time { get; set; }
        public int Flag { get; set; }
      

    }
    public class Pressure
    {
        public int id { get; set; }
        public string value { get; set; }
        public string time { get; set; }
        public int Flag { get; set; }


    }
    public class HeartRateData
    {
        public string value { get; set; }
        public string time { get; set; }

    }
    public class ElderHD
    {
        public int id { get; set; }
        public int  value { get; set; }

        public DateTime time { get; set; }

    }
}
