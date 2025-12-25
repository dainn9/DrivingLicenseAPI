using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrivingLicense.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrivingLicense.Infrastructure.Data.Configurations
{
    public class LicenseTypeConfiguration : IEntityTypeConfiguration<LicenseType>
    {
        public void Configure(EntityTypeBuilder<LicenseType> builder)
        {
            builder.ToTable("LicenseTypes");

            builder.HasKey(lt => lt.LicenseTypeId);

            builder.HasIndex(lt => lt.LicenseTypeName)
                   .IsUnique();

            builder.Property(lt => lt.LicenseTypeName)
                   .HasMaxLength(50);
        }
    }
}