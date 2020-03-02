﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using InBed.Entity;

namespace InBed.Data.Config
{
    /// <summary>
    /// 用户表配置
    /// </summary>
    public class UserConfig : EntityTypeConfiguration<UserEntity>
    {
        public UserConfig()
        {
            ToTable("Users");
            HasKey(item => item.Id);
            Property(item => item.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(item => item.FacilitatorCode).HasColumnType("varchar").IsRequired().HasMaxLength(20);
            Property(item => item.FacilitatorName).HasColumnType("varchar").IsRequired().HasMaxLength(120);
            Property(item => item.LoginName).HasColumnType("varchar").IsRequired().HasMaxLength(20);
            Property(item => item.Email).HasColumnType("varchar").IsRequired().HasMaxLength(36);
            Property(item => item.Password).HasColumnType("varchar").IsRequired().HasMaxLength(36);
            Property(item => item.RealName).HasColumnType("nvarchar").IsRequired().HasMaxLength(20);
            Property(item => item.Status).IsRequired();
        }
    }
}
