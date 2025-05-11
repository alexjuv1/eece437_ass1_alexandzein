namespace UniversityApp.Application.DTOs
{
    public class CourseDto
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public int DepartmentId { get; set; }
        public int ProfessorId { get; set; }
    }
}
