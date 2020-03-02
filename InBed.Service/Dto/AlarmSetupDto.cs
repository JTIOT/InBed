using InBed.Core;
using InBed.Core.Extentions;
using InBed.Service.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InBed.Service.Dto
{
    public class AlarmSetupDto : BaseDto
    {
        [DisplayName("服务商编号*"), Required, StringLength(20, MinimumLength = 5, ErrorMessage = "长度在5-20个字符之间")]
        public string FacilitatorCode { get; set; }
        [DisplayName("服务商名*"), Required, StringLength(120, MinimumLength = 5, ErrorMessage = "长度在5-120个字符之间")]
        public string FacilitatorName { get; set; }
        [DisplayName("老人姓名*")]
        public string ElderName { get; set; }
        [DisplayName("出生日期*")]
        public string birthday { get; set; }
        [DisplayName("心率上限值*")]
        public int maxheart { get; set; }
        [DisplayName("心率下限值*")]
        public int minheart { get; set; }
        [DisplayName("夜间离床时长*")]
        public int leavebedtime { get; set; }
        [DisplayName("离床检测开始")]
        public string s_time { get; set; }
        [DisplayName("离床检测结束")]
        public string e_time { get; set; }
        [DisplayName("呼吸上限值*")]
        public int maxbreath { get; set; }
        [DisplayName("呼吸下限值*")]
        public int minbreath { get; set; }
        [DisplayName("呼吸呼吸暂停（分钟）*")]
        public int pausebreath { get; set; }
        [DisplayName("卧床时长（小时/天）*")]
        public int inbedtime { get; set; }
        [DisplayName("系统设备类型")]
        public SetType set_type { get; set; }

        public int ElderId { get; set; }
        public int Age
        {
            get
            {
                if (!StringExtension.IsBlank(birthday))
                {
                    return ClassUtility.CalculateAge(DateTime.Parse(birthday), DateTime.Now);

                }
                else
                {
                    return 0;
                }

            }
        }
        public string typeName
        {
            get { return set_type.ToString(); }
        }

    }
}
