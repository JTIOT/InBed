using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Service.Dto
{
    public  class MessageDto
    {
        public int ElderID { get; set; }
        public string ElderName{ get; set; }
        public string  OutBedTime { get; set; }
        public string Message { get; set; }
    }
}
