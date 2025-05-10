// Models/Student.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniversityApp.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Student name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Range(2000, 2050, ErrorMessage = "Enrollment year must be between 2000 and 2050.")]
        public int EnrollmentYear { get; set; }

        // Foreign key
        [Required]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        // Many-to-many with Courses
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
}
