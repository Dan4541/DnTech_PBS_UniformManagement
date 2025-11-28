using DnTech_PBS_UniformManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnTech_PBS_UniformManagement.Data.Configurations
{
    public class HealthAreaConfiguration : IEntityTypeConfiguration<HealthArea>
    {
        public void Configure(EntityTypeBuilder<HealthArea> builder)
        {
            builder.HasKey(h => h.Id);

            builder.Property(h => h.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(h => h.Code)
                .HasMaxLength(20);

            builder.Property(h => h.Active)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(h => h.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.HasIndex(h => new { h.ProvinceId, h.Name })
                .IsUnique();

            builder.HasIndex(h => h.Code)
                .IsUnique()
                .HasFilter("[Code] IS NOT NULL");

            // Relationship
            builder.HasOne(h => h.Province)
                .WithMany(p => p.HealthAreas)
                .HasForeignKey(h => h.ProvinceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
