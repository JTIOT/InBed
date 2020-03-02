using System;
using System.Collections.Generic;
using InBed.Entity.Base;
using Newtonsoft.Json;

namespace InBed.Entity
{
    /// <summary>
    /// 用户实体
    /// </summary>
    public class UserEntity : BaseEntity
    {
        /// <summary>
        /// 服务商名
        /// </summary>
        public string FacilitatorName { get; set; }
        /// <summary>
        /// 服务商编号
        /// </summary>
        public string FacilitatorCode { get; set; }
        /// <summary>
        /// 登录账号
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public byte Status { get; set; }
        
        /// <summary>
        /// 用户拥有的角色
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<UserRoleEntity> UserRoles { get; set; }


    }
}
