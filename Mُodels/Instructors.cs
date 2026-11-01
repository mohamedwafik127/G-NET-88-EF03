using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ConsoleApp11
{
    public class Instructors
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string? BIO{ get; set; } 
        public DateTime JoinedDate { get; set; }
        public ICollection<Courses> Courses { get; set; } = new List<Courses>();
    }
}
