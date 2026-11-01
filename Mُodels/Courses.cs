using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp11
{
    public class Courses
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public CourseLevel Level { get; set; }
        public bool IsPublished { get; set; }
        public DateTime? PublishedDate { get; set; }
        public int CategoryId { get; set; }
        public Categories Category { get; set; } = null!;
        public int InstructorId { get; set; }
        public Instructors Instructor { get; set; } = null!;
        public ICollection<Enrollments> Enrollment { get; set; } = new List<Enrollments>();
    }
}

