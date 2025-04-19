// Models/Department.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace UniversityApp.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Department
    {
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Department name is required.")]
        public string Name { get; set; }
        public ICollection<Professor> Professors { get; set; } = new List<Professor>();
        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
