using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Service.Dto
{
    public class HearbeatStatusDto
    {
        public int Id { get; set; }
        public long DeviceID { get; set; }
        public int HeartbeatRate { get; set; }
        public int BreathingRate { get; set; }
        public int PressureValue { get; set; }
        public int HaveHR { get; set; }
        public int FileIndex { get; set; }
        public int SleepActivity { get; set; }
        public DateTime TimeStamp { get; set; }
        public DateTime ReceiveTime { get; set; }
    }
}
