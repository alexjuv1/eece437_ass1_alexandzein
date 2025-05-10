using System.ComponentModel.DataAnnotations;
namespace UniversityApp.DTOs
{
    public class ProfessorCreateDto
    {
        [Required]
        public string Name { get; set; } = "";

        [Required, EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        public int DepartmentId { get; set; }
    }
}