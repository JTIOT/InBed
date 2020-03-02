using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Entity
{
   public class RealTimeEntity
    {
        public int ID { get; set; }
        public int HeartbeatRate { get; set; }
        public int BreathingRate { get; set; }
        public int PressureValue { get; set; }
        public int SleepActivity { get; set; }
        public DateTime ReceiveTime { get; set; }
    }
}
