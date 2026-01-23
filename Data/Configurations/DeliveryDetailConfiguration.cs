using DnTech_PBS_UniformManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnTech_PBS_UniformManagement.Data.Configurations
{
    public class DeliveryDetailConfiguration : IEntityTypeConfiguration<DeliveryDetail>
    {
        public void Configure(EntityTypeBuilder<DeliveryDetail> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.UniformDeliveryId)
                .IsRequired();

            builder.Property(e => e.GarmentType)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Size)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(e => e.Quantity)
                .IsRequired()
                .HasDefaultValue(1);

            builder.Property(e => e.Notes)
                .HasMaxLength(200);

            // Relación con UniformDelivery
            builder.HasOne(e => e.UniformDelivery)
                .WithMany(ud => ud.DeliveryDetails)
                .HasForeignKey(e => e.UniformDeliveryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Índices
            builder.HasIndex(e => e.UniformDeliveryId);
            builder.HasIndex(e => e.GarmentType);
        }
    }
}
