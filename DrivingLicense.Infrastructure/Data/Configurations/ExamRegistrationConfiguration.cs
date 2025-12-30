using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrivingLicense.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrivingLicense.Infrastructure.Data.Configurations
{
    public class ExamRegistrationConfiguration : IEntityTypeConfiguration<ExamRegistration>
    {
        public void Configure(EntityTypeBuilder<ExamRegistration> builder)
        {
            builder.ToTable("ExamRegistrations");

            builder.HasKey(er => er.Id);

            builder.Property(er => er.Id)
                   .HasColumnName("ExamRegisId");

            builder.HasIndex(er => new { er.FileId, er.ExamSessionId })
                   .IsUnique();

            builder.HasOne(er => er.ExamSession)
                   .WithMany(es => es.ExamRegistrations)
                   .HasForeignKey(er => er.ExamSessionId);

            builder.HasOne(er => er.RegisterFile)
                   .WithMany(rf => rf.ExamRegistrations)
                   .HasForeignKey(er => er.FileId);
        }
    }
}