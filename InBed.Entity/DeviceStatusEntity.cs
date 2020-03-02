using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Entity
{
    public class DeviceStatusEntity
    {
        public int ID { get; set; }
        public string DevID { get; set; }
        public int IsBed { get; set; }
        public int IsOnline { get; set; }
        public int avHR { get; set; }
        public DateTime ReceiveTime { get; set; }
        public DateTime InBedTime { get; set; }
        
    }
}
