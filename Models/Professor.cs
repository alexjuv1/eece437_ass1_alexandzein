using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniversityApp.Models
{
    public class Professor
    {
        public int ProfessorId { get; set; }

        [Required(ErrorMessage = "Professor name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public Department? Department { get; set; } 

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
