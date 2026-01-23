using DnTech_PBS_UniformManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnTech_PBS_UniformManagement.Data.Configurations
{
    public class UniformDeliveryConfiguration : IEntityTypeConfiguration<UniformDelivery>
    {
        public void Configure(EntityTypeBuilder<UniformDelivery> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.EmployeeId)
                .IsRequired();

            builder.Property(e => e.DeliveryDate)
                .IsRequired();

            builder.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(50)
                .HasDefaultValue("Sin entrega");

            builder.Property(e => e.Observations)
                .HasMaxLength(500);

            builder.Property(e => e.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            // Relación con EmployeeHealthArea
            builder.HasOne(e => e.Employee)
                .WithMany(emp => emp.UniformDeliveries)
                .HasForeignKey(e => new { e.EmployeeId, e.HealthAreaId })
                .OnDelete(DeleteBehavior.Cascade);

            // Índices
            builder.HasIndex(e => new { e.EmployeeId, e.HealthAreaId });
            builder.HasIndex(e => e.DeliveryDate);
            builder.HasIndex(e => e.Status);
        }
    }
}
