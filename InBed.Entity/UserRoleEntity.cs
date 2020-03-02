﻿
using InBed.Entity.Base;
using Newtonsoft.Json;

namespace InBed.Entity
{
    /// <summary>
    /// 用户角色关系实体
    /// </summary>
    public class UserRoleEntity : BaseEntity
    {

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 所属用户
        /// </summary>
        [JsonIgnore]
        public virtual UserEntity User { get; set; }
    }
}
