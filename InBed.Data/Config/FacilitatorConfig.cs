using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using InBed.Entity;

namespace InBed.Data.Config
{
    public class FacilitatorConfig: EntityTypeConfiguration<FacilitatorEntity>
    {
        public FacilitatorConfig()
        {
            ToTable("Facilitator");
            HasKey(item => item.Id);
            Property(item => item.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(item => item.code).HasColumnType("varchar").IsRequired().HasMaxLength(20);
            Property(item => item.name).HasColumnType("varchar").IsRequired().HasMaxLength(120);
            Property(item => item.number).IsRequired();
            Property(item => item.contacts).HasColumnType("varchar").IsRequired().HasMaxLength(20);
            Property(item => item.province).HasColumnType("varchar").IsRequired().HasMaxLength(20);
            Property(item => item.city).HasColumnType("nvarchar").IsRequired().HasMaxLength(20);
            Property(item => item.district).HasColumnType("nvarchar").IsRequired().HasMaxLength(20);
            Property(item => item.address).HasColumnType("nvarchar").IsRequired().HasMaxLength(120);
            Property(item => item.capital).IsRequired();
            Property(item => item.management).HasColumnType("nvarchar").IsRequired().HasMaxLength(200);
            Property(item => item.up_id).IsRequired();
        }
    }
}
