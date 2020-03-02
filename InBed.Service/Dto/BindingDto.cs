using InBed.Entity;
using InBed.Service.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Service.Dto
{
    public class BindingDto : BaseDto
    {
        public string Name { get; set; }
        public string birthday { get; set; }
        public SexEnum sex { get; set; }
        public string homephone { get; set; }
        public string number { get; set; }
        public DeviceType type { get; set; }
        public string  model { get; set; }
        public DeviceStatus status { get; set; }

        public int Age
        {
            get { return CalculateAge(DateTime.Parse(birthday), DateTime.Now); }
        }
        public int CalculateAge(DateTime birthDate, DateTime now)
        {
            int age = now.Year - birthDate.Year;
            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
            {
                age--;
            }
            return age;
        }
        public string sexName
        {
            get { return sex.ToString(); }
        }
        public string statusName
        {
            get { return status.ToString(); }
        }

        public string typeName
        {
            get { return type.ToString(); }
        }
        public string FacilitatorCode { get; set; }
        public string FacilitatorName { get; set; }

    }
}
