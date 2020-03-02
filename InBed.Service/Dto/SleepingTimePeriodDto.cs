using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Service.Dto
{
    public class SleepingTimePeriodDto
    {
        public int Id { get; set; }
        public int DeviceID { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime StopTime { get; set; }
        public int MaxHB { get; set; }
        public int MinHB { get; set; }
        public DateTime MaxHBTime { get; set; }
        public DateTime MinHBTime { get; set; }
        public int SleepType { get; set; }
        //持续时间
        public int Duration { get; set; }

    }
}
