using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp11
{
    public class Categories
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int? ParentCategoryId { get; set; }
        public Categories? ParentCategory { get; set; }
        public ICollection<Courses> Course { get; set; } = new List<Courses>();
        public ICollection<Categories> SubCategories { get; set; } = new List<Categories>();
    }
}
