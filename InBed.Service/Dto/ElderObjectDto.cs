using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Service.Dto
{
    public class ElderObjectDto
    {
        public ElderObjectDto()
        {
            OnBedRecord = new OnBedRecordDto();
            HearbeatStatus = new HearbeatStatusDto();
            SleepingTimePeriod = new SleepingTimePeriodDto();

        }
        public int Id { get; set; }
        public int DeviceID { get; set; }
        public string ElderName { get; set; }
        public byte[] ElderPhoto { get; set; }
        public OnBedRecordDto OnBedRecord { get; set; }
        public HearbeatStatusDto HearbeatStatus { get; set; }
        public SleepingTimePeriodDto SleepingTimePeriod { get; set; }

    }
}
