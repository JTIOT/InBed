using InBed.Core;
using InBed.Service.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Service.Dto
{
    public class TypeAlarmDto
    {
        public TypeAlarmDto()
        {
            alarmSetup = new AlarmSetupDto();
            data = new List<AlarmData>();
        }
        public int elderID { get; set; }
        public string elderName { get; set; }
        public SexEnum sex { get; set; }
        public string birthday { get; set; }
        public string homephone { get; set; }
        public string contacts { get; set; }
        public string c_telephone { get; set; }
        public string homeaddress { get; set; }
       
        //年龄
        public int Age
        {
            get { return ClassUtility.CalculateAge(DateTime.Parse(birthday), DateTime.Now); }
        }

        public AlarmSetupDto alarmSetup { get; set; }

        public List<AlarmData> data { get; set; }

    }
    public class AlarmData
    {
        public string value { get; set; }
        public string time { get; set; }
    }
}
