
using InBed.Entity.Base;

namespace InBed.Entity
{
    /// <summary>
    /// 角色实体
    /// </summary>
    public class RoleEntity : BaseEntity
    {
        /// <summary>
        /// 服务商编号
        /// </summary>
        public string FacilitatorCode { get; set; }
        /// <summary>
        /// 服务商名称
        /// </summary>
        public string FacilitatorName { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
