
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using InBed.Service.Enum;

namespace InBed.Service.Dto
{
    public class SystemElderSetupDto:BaseDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 服务商编号
        /// </summary>
        [DisplayName("服务商编号*"), Required, StringLength(20, MinimumLength = 5, ErrorMessage = "长度在5-20个字符之间")]
        public string FacilitatorCode { get; set; }
        /// <summary>
        /// 服务商名
        /// </summary>
        [DisplayName("服务商名*"), Required, StringLength(120, MinimumLength = 5, ErrorMessage = "长度在5-120个字符之间")]
        public string FacilitatorName { get; set; }
        /// <summary>
        /// 老人ID
        /// </summary>
        [DisplayName("老人ID*"), Required]
        public int ElderID { get; set; }
        /// <summary>
        /// 老人姓名
        /// </summary>
        [DisplayName("老人姓名*"), Required, StringLength(20, MinimumLength = 5, ErrorMessage = "长度在5-20个字符之间")]
        public string ElderName { get; set; }

        /// <summary>
        /// 开始使用时间
        /// </summary>
        [DisplayName("开始使用时间*"), Required]
        public string start_data { get; set; }
        /// <summary>
        /// 结束使用时间
        /// </summary>
        [DisplayName("结束使用时间*"), Required]
        public string end_data { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [DisplayName("是否启用*"), Required]
        public bool is_enable { get; set; }



    }
}
