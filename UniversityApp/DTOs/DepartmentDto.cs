namespace UniversityApp.DTOs
{
    public class DepartmentDto
    {
        public int DepartmentId { get; set; }
        public string Name     { get; set; } = "";
        public IList<CourseDto> Courses      { get; set; } = new List<CourseDto>();
        public IList<ProfessorDto> Professors{ get; set; } = new List<ProfessorDto>();
    }
}
