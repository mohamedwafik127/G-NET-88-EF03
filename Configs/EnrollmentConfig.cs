using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp11
{
    public class EnrollmentConfig : IEntityTypeConfiguration<Enrollments>
    {
        public void Configure(EntityTypeBuilder<Enrollments> builder)
        {
            builder.HasKey(e => new { e.StudentsId, e.CoursesId });
            builder.Property(e => e.FinalGrade).HasPrecision(5,2);
            builder.Property(e => e.Progress).HasPrecision(18, 2);

            builder.HasOne(e => e.Student)
                   .WithMany(s => s.Enrollments)
                   .HasForeignKey(e => e.StudentsId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Course)
                    .WithMany(c => c.Enrollment)
                    .HasForeignKey(e => e.CoursesId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
