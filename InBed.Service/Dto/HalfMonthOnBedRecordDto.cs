using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Service.Dto
{
    public class HalfMonthOnBedRecordDto
    {
        public int ProductID { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int IsBed { get; set; }
    }
}
