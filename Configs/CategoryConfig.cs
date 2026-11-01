using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp11
{
    public class CategoryConfig : IEntityTypeConfiguration<Categories>
    {
        public void Configure(EntityTypeBuilder<Categories> builder)
        {
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Description).HasMaxLength(500);

            builder.HasOne(c => c.ParentCategory)
                   .WithMany(c => c.SubCategories)
                   .HasForeignKey(c => c.ParentCategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(i => i.Course)
                   .WithOne(c => c.Category)
                   .HasForeignKey(c => c.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
     new Categories { Id = 1, Name = "Programming", Description = "Software development courses", ParentCategoryId = null },
     new Categories { Id = 2, Name = "Design", Description = "Design and creativity courses", ParentCategoryId = null },

     new Categories { Id = 3, Name = "Backend", Description = "Server-side development", ParentCategoryId = 1 },
     new Categories { Id = 4, Name = "Frontend", Description = "Client-side development", ParentCategoryId = 1 },

     new Categories { Id = 5, Name = "ASP.NET", Description = "Web development with .NET", ParentCategoryId = 3 }
 );


        }
    }
    }

