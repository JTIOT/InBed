using InBed.Core;
using InBed.Service.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Service.Dto
{
   public class ElderInfoDto
    {
        public ElderInfoDto()
        {
            AlarmSetup = new AlarmSetupDto();
        }

        public int ElderID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string ElderName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public SexEnum sex { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string HomePhone { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string HomeAdderss { get; set; }
        /// <summary>
        /// 出生年月
        /// </summary>
        public string birthday { get; set; }
        /// <summary>
        /// 心率
        /// </summary>
        public int HD { get; set; }
        /// <summary>
        /// 是否在床
        /// </summary>
        public int Inbed { get; set; }

        public DateTime InbedTime { get; set; }
        /// <summary>
        /// 设备是否在线
        /// </summary>
        public int Online { get; set; }
        public string MapPoint { get; set; }
        public int Falg { get; set; }
        
        public int PressureValue { get; set; }
        public string  SexName
        {
            get {
                return sex.ToString();
            }
        }
        public int Age
        {
            get { return ClassUtility.CalculateAge(DateTime.Parse(birthday), DateTime.Now); }
        }
        public AlarmSetupDto AlarmSetup { get; set; }
    }
}
