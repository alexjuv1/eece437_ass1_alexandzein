using System.Collections.Generic;

namespace UniversityApp.Core.Entities
{
    public class Department
    {
        public int DepartmentId { get; set; }

        // matches Models/Department.Name
        public string Name { get; set; }

        // reverse navs
        public ICollection<Professor> Professors { get; set; }
            = new List<Professor>();

        public ICollection<Course> Courses { get; set; }
            = new List<Course>();

        public ICollection<Student> Students { get; set; }
            = new List<Student>();
    }
}
