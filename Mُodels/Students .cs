using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp11
{
    public class Students
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public DateTime DOB { get; set; }
        public ICollection<Enrollments> Enrollments { get; set; } = new List<Enrollments>();
    }
}
