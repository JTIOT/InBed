using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Entity
{
    public class HalfMonthOnBedRecordEntity
    {
        public int ProductID { get; set; }
        public string Date { get; set; }
        public string OnBedTime { get; set; }
        public string LeaveBedTime { get; set; }
    }
    public class DayOnBedRecordEntity
    {
        public int ProductID { get; set; }
        public string Date { get; set; }
        public string OnBedTime { get; set; }
        public string LeaveBedTime { get; set; }
    }

    public class OutBedRecordCountEntity
    {
        public int count { get; set; }

    }
}
