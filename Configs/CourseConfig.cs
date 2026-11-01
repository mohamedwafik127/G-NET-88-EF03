using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp11
{
    public class CourseConfig : IEntityTypeConfiguration<Courses>
    {
        public void Configure(EntityTypeBuilder<Courses> builder)
        {
            builder.Property(c => c.Title).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Description).HasMaxLength(500);
            builder.Property(c => c.Price).IsRequired().HasPrecision(18, 2);

            builder.HasData(
                new Courses { Id = 1, Title = "C# Fundamentals", Description = "Learn the basics of C#.", Price = 300m, Level = CourseLevel.Beginner, IsPublished = true, PublishedDate = new DateTime(2024, 1, 10), InstructorId = 1, CategoryId = 1 },
                new Courses { Id = 2, Title = "ASP.NET Core Web API", Description = "Build REST APIs with ASP.NET Core.", Price = 750m, Level = CourseLevel.Intermediate, IsPublished = true, PublishedDate = new DateTime(2024, 3, 5), InstructorId = 1, CategoryId = 5 },
                new Courses { Id = 3, Title = "Advanced EF Core", Description = "Performance and advanced mapping.", Price = 900m, Level = CourseLevel.Advanced, IsPublished = false, PublishedDate = null, InstructorId = 1, CategoryId = 3 },
                new Courses { Id = 4, Title = "HTML and CSS Basics", Description = "Build your first web pages.", Price = 250m, Level = CourseLevel.Beginner, IsPublished = true, PublishedDate = new DateTime(2024, 2, 1), InstructorId = 3, CategoryId = 4 },
                new Courses { Id = 5, Title = "UI/UX Design Principles", Description = null, Price = 400m, Level = CourseLevel.Beginner, IsPublished = true, PublishedDate = new DateTime(2024, 4, 12), InstructorId = 2, CategoryId = 2 },
                new Courses { Id = 6, Title = "Design Systems", Description = "Create scalable design systems.", Price = 850m, Level = CourseLevel.Advanced, IsPublished = false, PublishedDate = null, InstructorId = 2, CategoryId = 2 }
            );
        }
    }
}
