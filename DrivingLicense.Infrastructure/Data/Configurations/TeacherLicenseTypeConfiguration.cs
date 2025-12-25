using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrivingLicense.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrivingLicense.Infrastructure.Data.Configurations
{
    public class TeacherLicenseTypeConfiguration : IEntityTypeConfiguration<TeacherLicenseType>
    {
        public void Configure(EntityTypeBuilder<TeacherLicenseType> builder)
        {
            builder.ToTable("TeacherLicenseTypes");

            builder.HasKey(tlt => tlt.Id);

            builder.HasIndex(tlt => new { tlt.TeacherId, tlt.LicenseTypeId }).IsUnique();

            builder.HasOne(tlt => tlt.Teacher)
                   .WithMany(t => t.TeacherLicenseTypes)
                   .HasForeignKey(tlt => tlt.TeacherId);

            builder.HasOne(tlt => tlt.LicenseType)
                   .WithMany(lt => lt.TeacherLicenseTypes)
                   .HasForeignKey(tlt => tlt.LicenseTypeId);
        }
    }
}