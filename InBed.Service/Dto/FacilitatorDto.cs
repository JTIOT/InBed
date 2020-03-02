using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using InBed.Service.Enum;

namespace InBed.Service.Dto
{
    /// <summary>
    /// 服务商Dto
    /// </summary>
    public class FacilitatorDto : BaseDto
    {
        [DisplayName("服务商编号*"), Required, StringLength(20, MinimumLength = 5, ErrorMessage = "长度在5-20个字符之间")]
        public string Code { get; set; }
        [DisplayName("服务商名*"), Required, StringLength(120, MinimumLength = 5, ErrorMessage = "长度在5-120个字符之间")]
        public string Name { get; set; }
        [DisplayName("工商注册编号*"), Required, StringLength(20, MinimumLength = 5, ErrorMessage = "长度在5-120个字符之间")]
        public string Number { get; set; }
        [DisplayName("联系人*"), Required, StringLength(20, MinimumLength = 2, ErrorMessage = "长度在2-20个字符之间")]
        public string Contacts { get; set; }
        [DisplayName("省份*"), Required, StringLength(20, MinimumLength = 2, ErrorMessage = "长度在2-20个字符之间")]
        public string Province { get; set; }
        [DisplayName("城市*"), Required, StringLength(20, MinimumLength = 2, ErrorMessage = "长度在2-20个字符之间")]
        public string City { get; set; }
        [DisplayName("区/县*"), Required, StringLength(20, MinimumLength = 2, ErrorMessage = "长度在2-20个字符之间")]
        public string District { get; set; }
        [DisplayName("详细地址*"), Required, StringLength(120, MinimumLength = 2, ErrorMessage = "长度在2-120个字符之间")]
        public string Address { get; set; }
        [DisplayName("注册资金(w)"), Required]
        public int Capital { get; set; }
        [Required, DisplayName("经营范围"), MinLength(1), MaxLength(500)]
        public string Management { get; set; }
    
        [DisplayName("上级服务商")]
        public int? UP_id { get; set; }
        [DisplayName("服务商等级")]
        public FacilitatorLevel F_Level { get; set; }

        public string LevelName {
            get {
                return F_Level.ToString();
            }

        }
    }
}
