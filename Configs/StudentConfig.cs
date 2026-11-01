using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp11
{
    public class StudentConfig : IEntityTypeConfiguration<Students>
    {
        public void Configure(EntityTypeBuilder<Students> builder)
        {
            builder.Property(s => s.FullName).IsRequired().HasMaxLength(100);
            builder.Property(s => s.EmailAddress).IsRequired().HasMaxLength(100);
        }
    }
}
