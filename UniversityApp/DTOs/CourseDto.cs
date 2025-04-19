namespace UniversityApp.DTOs
{
    public class CourseDto
    {
        public int CourseId      { get; set; }
        public string Name       { get; set; } = "";
        public int Credits       { get; set; }
        public string DepartmentName { get; set; } = "";
        public string ProfessorName  { get; set; } = "";
        public IList<StudentDto> Students { get; set; } = new List<StudentDto>();
    }
}
