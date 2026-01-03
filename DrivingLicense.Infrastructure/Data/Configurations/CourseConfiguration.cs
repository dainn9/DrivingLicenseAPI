using DrivingLicense.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrivingLicense.Infrastructure.Data.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                   .HasColumnName("CourseId");

            builder.Property(c => c.CourseName)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(c => c.CourseName)
                   .IsUnique();

            builder.Property(c => c.StartDate)
                   .IsRequired();

            builder.Property(c => c.EndDate)
                   .IsRequired();

            builder.Property(c => c.Capacity)
                   .IsRequired();

            builder.Property(c => c.Status)
                   .IsRequired();

            builder.ToTable(c => c.HasCheckConstraint("CK_Courses_Capacity_NonNegative", "[Capacity] >= 0"));

            builder.ToTable(c => c.HasCheckConstraint("CK_Courses_ValiDates", "[StartDate] >= GETDATE() && [EndDate] > [StartDate]"));

            builder.HasOne(c => c.LicenseType)
                   .WithMany(lt => lt.Courses)
                   .HasForeignKey(c => c.LicenseTypeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.RegisterFiles)
                     .WithOne(rf => rf.Course)
                     .HasForeignKey(rf => rf.CourseId)
                     .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.TeachingSchedules)
                     .WithOne(ts => ts.Course)
                     .HasForeignKey(ts => ts.CourseId)
                     .OnDelete(DeleteBehavior.Restrict);
        }
    }
}