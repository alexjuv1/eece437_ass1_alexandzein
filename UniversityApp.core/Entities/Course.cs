using System.Collections.Generic;

namespace UniversityApp.Core.Entities
{
    public class Course
    {
        public int CourseId { get; set; }

        // matches Models/Course.Name
        public string Name { get; set; }

        public int Credits { get; set; }

        // FK to Department
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        // FK to Professor
        public int ProfessorId { get; set; }
        public Professor Professor { get; set; }

        // join to Student
        public ICollection<StudentCourse> StudentCourses { get; set; }
            = new List<StudentCourse>();
    }
}
