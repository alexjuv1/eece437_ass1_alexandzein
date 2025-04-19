// Models/ViewModels/CourseViewModel.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniversityApp.Models;

namespace UniversityApp.ViewModels
{
    public class CourseViewModel
    {
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Course name is required.")]
        public string Name { get; set; }

        [Range(1, 6, ErrorMessage = "Credits must be between 1 and 6.")]
        public int Credits { get; set; }

        [Required(ErrorMessage = "You must select a department.")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "You must select a professor.")]
        public int ProfessorId { get; set; }

        // For dropdowns
        public List<Department>? Departments { get; set; }
        public List<Professor>? Professors { get; set; }
    }
}
