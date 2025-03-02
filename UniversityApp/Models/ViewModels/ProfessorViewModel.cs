using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniversityApp.Models;

namespace UniversityApp.ViewModels
{
    public class ProfessorViewModel
    {
        public int ProfessorId { get; set; }

        [Required(ErrorMessage = "Professor name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "You must select a department.")]
        public int DepartmentId { get; set; }

        public List<Department>? Departments { get; set; }
    }
}
