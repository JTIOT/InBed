﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using InBed.Entity;

namespace InBed.Data.Config
{
    /// <summary>
    /// 角色表配置
    /// </summary>
    public class UserRoleConfig : EntityTypeConfiguration<UserRoleEntity>
    {
        public UserRoleConfig()
        {
            ToTable("UserRole");
            HasKey(item => item.Id);
            Property(item => item.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(item => item.RoleId).IsRequired();
            Property(item => item.UserId).IsRequired();
            HasRequired(item => item.User).WithMany(item => item.UserRoles).HasForeignKey(item => item.UserId);
        }
    }
}
