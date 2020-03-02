using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Entity
{
    public class AlarmEntity
    {
        public int ID { get; set; }
        public string  DeviceNumber { get; set; }
        public int AlarmType { get; set; }
        public string AlarmTime { get; set; }
        public string UploadTime { get; set; }
        public string OnLineTime { get; set; }
        public string AlarmReason { get; set; }
        public int IsHandle { get; set; }
        public string HandleOpinions { get; set; }
        public int Modifier { get; set; }
        public string ElderName { get; set; }
        public int ElderId { get; set; }

    }
}
