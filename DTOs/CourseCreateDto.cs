using System.ComponentModel.DataAnnotations;
namespace UniversityApp.DTOs
{
    public class CourseCreateDto
    {
        [Required]
        public string Name { get; set; } = "";

        [Required]
        [Range(1, 10)]
        public int Credits { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public int ProfessorId { get; set; }
    }
}