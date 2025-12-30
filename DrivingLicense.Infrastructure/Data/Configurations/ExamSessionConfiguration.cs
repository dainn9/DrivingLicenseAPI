using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrivingLicense.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrivingLicense.Infrastructure.Data.Configurations
{
    public class ExamSessionConfiguration : IEntityTypeConfiguration<ExamSession>
    {
        public void Configure(EntityTypeBuilder<ExamSession> builder)
        {
            builder.ToTable("ExamSessions");

            builder.HasKey(es => es.Id);

            builder.Property(es => es.Id)
                .HasColumnName("ExamSessionId");

            builder.ToTable(es => es.HasCheckConstraint("CK_ExamSessions_TimeRange", "[StartTime] < [EndTime]"));

            builder.HasOne(es => es.LicenseType)
                   .WithMany(lt => lt.ExamSessions)
                   .HasForeignKey(es => es.LicenseTypeId);
        }
    }
}