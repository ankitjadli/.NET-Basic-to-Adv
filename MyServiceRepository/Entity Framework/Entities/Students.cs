using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServiceRepository.Entity_Framework.Entities
{
    public class Students
    {
        [Key] // Annotations to configure model
        public int Id { get; set; }

        public string Name { get; set; }

        [MaxLength(100)]
        public int result { get; set; }

        [Required]
        public string Student_class { get; set; }
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Course> Courses { get; set; }

        public StudentAddress Address { get; set; } // Navigation property
    }

    public class StudentAddress
    {
        public int Id { get; set; } // This should match Student.Id
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }

    public class StudentLazy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }

    public class Course
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
