using System.Collections.Generic;

namespace UniversityApp.Core.Entities
{
    public class Professor
    {
        public int ProfessorId { get; set; }

        // matches Models/Professor.Name
        public string Name { get; set; }

        public string Email { get; set; }

        // FK to Department
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        // their Courses
        public ICollection<Course> Courses { get; set; }
            = new List<Course>();
    }
}
