// Models/Course.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
namespace UniversityApp.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Course
    {
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Course name is required.")]
        public string Name { get; set; }

        [Range(1, 6, ErrorMessage = "Credits must be between 1 and 6.")]
        public int Credits { get; set; }

        [Required]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; } 

        [Required]
        public int ProfessorId { get; set; }
        public Professor? Professor { get; set; } 

        // Many-to-many with Students
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
}
