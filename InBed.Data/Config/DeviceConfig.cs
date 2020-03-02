using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using InBed.Entity;

namespace InBed.Data.Config
{
   public class DeviceConfig : EntityTypeConfiguration<DeviceEntity>
   {
        public DeviceConfig()
        {
            ToTable("Device");
            HasKey(item => item.Id);
            Property(item => item.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(item => item.Facilitator_Code).HasColumnType("varchar").IsRequired().HasMaxLength(20);
            Property(item => item.Facilitator_Name).HasColumnType("varchar").IsRequired().HasMaxLength(20);
            Property(item => item.device_number).IsRequired();
            Property(item => item.device_type).IsRequired();
            Property(item => item.device_model).IsRequired();
            Property(item => item.is_enable).IsRequired();
            Property(item => item.status).IsRequired();
        }

    }
}
