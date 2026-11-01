using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp11
{
    public class Enrollments
    {
        public int StudentsId { get; set; }
        public int CoursesId { get; set; }
        public decimal Progress { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public EnrollmentStatus EnrollmentStatus { get; set; }
        public decimal? FinalGrade { get; set; }
        public Students Student { get; set; } = null!;
        public Courses Course { get; set; } = null!;
    }
}
