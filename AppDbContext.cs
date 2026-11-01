using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp11
{
    internal class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=Online Learning Platform;Trusted_Connection=True;TrustServerCertificate = true");
        }
        public DbSet<Students> Students { get; set; } = null!;
        public DbSet<Enrollments> Enrollments { get; set; } = null!;
        public DbSet<Courses> Courses { get; set; } = null!;
        public DbSet<Instructors> Instructors { get; set; } = null!;
        public DbSet<Categories> Categories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new InstructorConfig());
            modelBuilder.ApplyConfiguration(new CategoryConfig());
            modelBuilder.ApplyConfiguration(new CourseConfig());
            modelBuilder.ApplyConfiguration(new StudentConfig());
            modelBuilder.ApplyConfiguration(new EnrollmentConfig());
        }
    }
}
