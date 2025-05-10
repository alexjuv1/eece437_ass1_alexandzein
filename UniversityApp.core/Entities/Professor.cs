using System.Collections.Generic;

namespace UniversityApp.Core.Entities
{
    public class Professor
    {
        public int ProfessorId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}