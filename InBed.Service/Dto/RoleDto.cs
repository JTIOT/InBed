

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InBed.Service.Dto
{
    /// <summary>
    /// 角色DTO
    /// </summary>
    public class RoleDto : BaseDto
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [Required, DisplayName("角色名称"), MinLength(2), MaxLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Required, DisplayName("描述"), MinLength(1), MaxLength(50)]
        public string Description { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [Required, DisplayName("服务商编号"), MinLength(1), MaxLength(50)]
        public string FacilitatorCode { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [Required, DisplayName("服务商名称"), MinLength(1), MaxLength(150)]
        public string FacilitatorName { get; set; }
    }
}
