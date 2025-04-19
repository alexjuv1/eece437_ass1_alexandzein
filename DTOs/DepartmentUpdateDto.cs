using System.ComponentModel.DataAnnotations;

namespace UniversityApp.DTOs
{
    public class DepartmentUpdateDto
    {
        [Required]
        public string Name { get; set; } = "";
    }
}
