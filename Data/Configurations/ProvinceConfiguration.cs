using DnTech_PBS_UniformManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnTech_PBS_UniformManagement.Data.Configurations
{
    public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Code)
                .HasMaxLength(20);

            builder.Property(p => p.Active)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(p => p.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.HasIndex(p => p.Name)
                .IsUnique();

            builder.HasIndex(p => p.Code)
                .IsUnique()
                .HasFilter("[Code] IS NOT NULL");

            // Relationship
            builder.HasMany(p => p.HealthAreas)
                .WithOne(h => h.Province)
                .HasForeignKey(h => h.ProvinceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
