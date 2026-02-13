using DrivingLicense.Domain.Entities;
using DrivingLicense.Infrastructure.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace DrivingLicense.Infrastructure.Data
{
    public class DrivingDbContext : IdentityDbContext<AppUser>
    {
        public DrivingDbContext(DbContextOptions<DrivingDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses => Set<Course>();
        public DbSet<ExamRegistration> ExamRegistrations => Set<ExamRegistration>();
        public DbSet<ExamResult> ExamResults => Set<ExamResult>();
        public DbSet<ExamSession> ExamSessions => Set<ExamSession>();
        public DbSet<LicenseType> LicenseTypes => Set<LicenseType>();
        public DbSet<RegisterFile> RegisterFiles => Set<RegisterFile>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<TeacherLicenseType> TeacherLicenseTypes => Set<TeacherLicenseType>();
        public DbSet<TeachingSchedule> TeachingSchedules => Set<TeachingSchedule>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        // Other DbSet properties for your entities go here

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Apply configurations from the Configurations folder
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DrivingDbContext).Assembly);
        }
    }
}