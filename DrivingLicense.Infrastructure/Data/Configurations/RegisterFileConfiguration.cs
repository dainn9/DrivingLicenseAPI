using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrivingLicense.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrivingLicense.Infrastructure.Data.Configurations
{
    public class RegisterFileConfiguration : IEntityTypeConfiguration<RegisterFile>
    {
        public void Configure(EntityTypeBuilder<RegisterFile> builder)
        {
            builder.ToTable("RegisterFiles");

            builder.HasKey(rf => rf.RegisterFileId);

            builder.Property(rf => rf.SubmissionDate)
                    .IsRequired();

            builder.HasOne(rf => rf.Course)
                   .WithMany(c => c.RegisterFiles)
                   .HasForeignKey(rf => rf.CourseId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(rf => rf.LicenseType)
                   .WithMany(lt => lt.RegisterFiles)
                    .HasForeignKey(rf => rf.LicenseTypeId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(rf => rf.Student)
                     .WithMany(s => s.RegisterFiles)
                     .HasForeignKey(rf => rf.StudentId)
                     .OnDelete(DeleteBehavior.Restrict);
        }
    }
}