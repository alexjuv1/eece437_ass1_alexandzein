using System.ComponentModel.DataAnnotations;
namespace UniversityApp.DTOs
{
    public class DepartmentCreateDto
    {
        [Required]
        public string Name { get; set; } = "";
    }
}
