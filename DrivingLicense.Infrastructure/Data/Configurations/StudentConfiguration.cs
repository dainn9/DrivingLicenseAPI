using DrivingLicense.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrivingLicense.Infrastructure.Data.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");

            builder.HasKey(s => s.Id);

            builder.HasIndex(s => s.Email)
                   .IsUnique()
                    .HasFilter("[Email] IS NOT NULL");

            builder.Property(s => s.FullName)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(s => s.Email)
                   .HasMaxLength(50);

            builder.HasIndex(s => s.PhoneNumber)
                   .IsUnique();

            builder.Property(s => s.PhoneNumber)
                   .HasMaxLength(10);

            builder.Property(s => s.Address)
                   .HasMaxLength(200);

            builder.HasIndex(s => s.IdentityCard)
                    .IsUnique();

            builder.Property(s => s.IdentityCard)
                   .HasMaxLength(12);
        }
    }
}