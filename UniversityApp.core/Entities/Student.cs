using System;
using System.Collections.Generic;

namespace UniversityApp.Core.Entities
{
    public class Student
    {
        public int StudentId { get; set; }

        // matches Models/Student.Name
        public string Name { get; set; }

        public string Email { get; set; }

        // matches Models/Student.EnrollmentYear
        public int EnrollmentYear { get; set; }

        // FK to Department
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        // join to Course
        public ICollection<StudentCourse> StudentCourses { get; set; }
            = new List<StudentCourse>();
    }
}
