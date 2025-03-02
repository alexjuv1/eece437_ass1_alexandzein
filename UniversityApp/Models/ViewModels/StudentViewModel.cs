using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniversityApp.Models;

namespace UniversityApp.ViewModels
{
    public class StudentViewModel
    {
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Student name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Range(2000, 2050, ErrorMessage = "Enrollment year must be between 2000 and 2050.")]
        public int EnrollmentYear { get; set; }

        [Required(ErrorMessage = "You must select a department.")]
        public int DepartmentId { get; set; }

        public List<Department>? Departments { get; set; }
    }
}
