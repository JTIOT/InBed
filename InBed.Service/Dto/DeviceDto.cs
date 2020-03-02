using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using InBed.Service.Enum;

namespace InBed.Service.Dto
{
    /// <summary>
    /// 设备
    /// </summary>
    public class DeviceDto: BaseDto
    {
        [DisplayName("服务商编号*"), Required, StringLength(20, MinimumLength = 5, ErrorMessage = "长度在5-20个字符之间")]
        public string FacilitatorCode { get; set; }
        [DisplayName("服务商名*"), Required, StringLength(120, MinimumLength = 5, ErrorMessage = "长度在5-120个字符之间")]
        public string FacilitatorName { get; set; }
        [DisplayName("设备编码*"), Required, StringLength(120, MinimumLength = 5, ErrorMessage = "长度在5-20个字符之间")]
        public string device_number { get; set; }
        [DisplayName("设备型号*")]
        public string device_model { get; set; }
        [DisplayName("启用状态*")]
        public bool is_enable { get; set; }

        [DisplayName("设备类型*")]
        public DeviceType device_type { get; set; }
        public string TypeName
        {
            get { return device_type.ToString(); }
        }
        [DisplayName("设备状态*"), Required]
        public DeviceStatus status { get; set; }
        public string statusName
        {
            get { return status.ToString(); }
        }


      
    }
}
