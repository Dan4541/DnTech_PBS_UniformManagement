using DnTech_PBS_UniformManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnTech_PBS_UniformManagement.Data.Configurations
{
    public class EmployeeHealthAreaConfiguration : IEntityTypeConfiguration<EmployeeHealthArea>
    {
        public void Configure(EntityTypeBuilder<EmployeeHealthArea> builder)
        {
            // Composite key
            builder.HasKey(e => new { e.EmployeeId, e.HealthAreaId });

            builder.Property(e => e.Position)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(e => e.AssignedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            // Relación con ApplicationUser (Employee)
            builder.HasOne(e => e.Employee)
                .WithMany()
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación con HealthArea
            builder.HasOne(e => e.HealthArea)
                .WithMany(h => h.EmployeeHealthAreas)
                .HasForeignKey(e => e.HealthAreaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Índices
            builder.HasIndex(e => e.HealthAreaId);
            builder.HasIndex(e => e.Position);
        }
    }
}
