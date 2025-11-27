using DnTech_PBS_UniformManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnTech_PBS_UniformManagement.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            // Properties
            builder.Property(u => u.Fullname)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(u => u.IdCard)
                .HasMaxLength(20);

            builder.Property(u => u.Active)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(u => u.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            // Indexes
            builder.HasIndex(u => u.IdCard)
                .IsUnique()
                .HasFilter("[IdCard] IS NOT NULL"); // Solo si no es null

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.HasIndex(u => u.Active);
        }
    }
}
