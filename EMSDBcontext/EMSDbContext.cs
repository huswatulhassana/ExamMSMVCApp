using ExamMSAppMVC.Models.Entities;
using ExamMSAppMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamMSAppMVC.EMSDBcontext
{
    public class EMSDbContext(DbContextOptions<EMSDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            SeedRoleData(builder);
            SeedAdminData(builder);

            // Result to Student Relationship (One-to-Many)
            builder.Entity<Result>()
                .HasOne(r => r.Student)
                .WithMany(s => s.Results)
                .HasForeignKey(r => r.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Question to Exam Relationship (One-to-Many)
            builder.Entity<Question>()
                .HasOne(q => q.Exam)
                .WithMany(e => e.Questions)
                .HasForeignKey(q => q.ExamId)
                .OnDelete(DeleteBehavior.Cascade);

            // User to Role Relationship (Many-to-One)
            builder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            // Course to Exam Relationship (One-to-Many)
            builder.Entity<Course>()
                .HasMany(c => c.Exams)
                .WithOne(e => e.Course)
                .HasForeignKey(e => e.CourseId);

            // User to Student Relationship (One-to-One) - FIXED
            builder.Entity<User>()
                .HasOne(u => u.StudentProfile)
                .WithOne(s => s.User)
                .HasForeignKey<Student>(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

                
        }

        private void SeedAdminData(ModelBuilder builder)
        {
            var adminUserId = new Guid("a45c9e02-1f0b-4e57-b3d8-9b77b4a302be");

            var adminRoleId = new Guid("6e3d4978-dcb0-42ea-8c48-7f6209d4a871");

            var adminUser = new User
            {
                Id = adminUserId,
                RoleId = adminRoleId,
                Email = "admin@ems.com",
                FirstName = "Anike",
                LastName = "Omotoyosi",
                RegistrationNumber = "ADMIN001",
                HashPassword = "ExamApp_2026_Project!",

                EmailConfirmed = true,
                DateCreated = DateTime.SpecifyKind(new DateTime(2026, 03, 12), DateTimeKind.Utc),
                DateModified = DateTime.SpecifyKind(new DateTime(2026, 03, 12), DateTimeKind.Utc)
            };

            builder.Entity<User>().HasData(adminUser);
        }

        private void SeedRoleData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = new Guid("6e3d4978-dcb0-42ea-8c48-7f6209d4a871"), // Admin Role
                    Name = "Admin",
                    DateCreated = DateTime.SpecifyKind(new DateTime(2026, 03, 12), DateTimeKind.Utc),
                    DateModified = DateTime.SpecifyKind(new DateTime(2026, 03, 12), DateTimeKind.Utc)
                },
                new Role
                {
                    // FIXED: Hardcoded Guid so it doesn't change on every build
                    Id = new Guid("d2959648-7494-4475-8022-772863955688"),

                    Name = "Student",
                    DateCreated = DateTime.SpecifyKind(new DateTime(2026, 03, 12), DateTimeKind.Utc),
                    DateModified = DateTime.SpecifyKind(new DateTime(2026, 03, 12), DateTimeKind.Utc)
                }
            );
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}