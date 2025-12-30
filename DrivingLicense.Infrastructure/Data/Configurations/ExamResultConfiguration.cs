using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrivingLicense.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrivingLicense.Infrastructure.Data.Configurations
{
    public class ExamResultConfiguration : IEntityTypeConfiguration<ExamResult>
    {
        public void Configure(EntityTypeBuilder<ExamResult> builder)
        {
            builder.ToTable("ExamResults");

            builder.HasKey(er => er.Id);

            builder.Property(er => er.Id)
                .HasColumnName("ResultId");

            builder.HasOne(er => er.ExamRegistration)
                .WithOne(er => er.ExamResult)
                .HasForeignKey<ExamResult>(er => er.ExamRegisId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}