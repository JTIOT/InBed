using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Entity
{
    public class ElderListEntity
    {
        public int id { get; set; }
        public string  name { get; set; }
        public string birthday { get; set; }
        public int sex { get; set; }
        public string homephone { get; set; }
        public string  homeaddress { get; set; }
        public string  MapPoint { get; set; }
        public int DeviceId { get; set; }
        public int avHR { get; set; }
        public int IsBed { get; set; }
        public int IsOnline { get; set; }
    }
}
