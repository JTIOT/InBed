using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Service.Dto
{
    //山下床记录
    public class OnBedRecordDto
    {

        public int Id { get; set; }
        public int DeviceID { get; set; }
        public DateTime OnBedTime { get; set; }
        public DateTime LeaveBedTime { get; set; }
        public DateTime CreateTime { get; set; }
        public bool IsBed { get; set; }
    }
}
