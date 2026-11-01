using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp11
{
    public class InstructorConfig : IEntityTypeConfiguration<Instructors>
    {
        public void Configure(EntityTypeBuilder<Instructors> builder)
        {
            builder.Property(i => i.FullName).IsRequired().HasMaxLength(100);
            builder.Property(i => i.EmailAddress).IsRequired().HasMaxLength(150);
            builder.Property(i => i.JoinedDate).IsRequired();
            builder.Property(i => i.BIO).HasMaxLength(500);

            builder.HasMany(i => i.Courses)
                   .WithOne(c => c.Instructor)
                   .HasForeignKey(c => c.InstructorId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new Instructors { Id = 1, FullName = "Ahmed Hassan", EmailAddress = "ahmed.hassan@example.com", BIO = "Senior .NET developer with 10 years of experience.", JoinedDate = new DateTime(2023, 1, 15) },
                new Instructors { Id = 2, FullName = "Sara Ali", EmailAddress = "sara.ali@example.com", BIO = null, JoinedDate = new DateTime(2023, 6, 1) },
                new Instructors { Id = 3, FullName = "Omar Khaled", EmailAddress = "omar.khaled@example.com", BIO = "Front-end specialist and UI designer.", JoinedDate = new DateTime(2024, 2, 20) }
            );
        }
    }
}
