using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrivingLicense.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrivingLicense.Infrastructure.Data.Configurations
{
    public class TeachingScheduleConfiguration : IEntityTypeConfiguration<TeachingSchedule>
    {
        public void Configure(EntityTypeBuilder<TeachingSchedule> builder)
        {
            builder.ToTable("TeachingSchedules");

            builder.HasKey(ts => ts.ScheduleId);

            builder.Property(ts => ts.StartTime)
                .IsRequired();

            builder.Property(ts => ts.EndTime)
                .IsRequired();

            builder.HasIndex(ts => new { ts.TeachingDate, ts.StartTime, ts.EndTime, ts.Location })
                .IsUnique();

            builder.ToTable(ts => ts.HasCheckConstraint("CK_TeachingSchedule_TimeRange", "[StartTime] < [EndTime]"));

            builder.HasOne(ts => ts.Teacher)
                .WithMany(t => t.TeachingSchedules)
                .HasForeignKey(ts => ts.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ts => ts.Course)
                .WithMany(c => c.TeachingSchedules)
                .HasForeignKey(ts => ts.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}