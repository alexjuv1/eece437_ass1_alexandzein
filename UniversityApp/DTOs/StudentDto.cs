namespace UniversityApp.DTOs
{
    public class StudentDto
    {
        public int StudentId     { get; set; }
        public string Name       { get; set; } = "";
        public string Email      { get; set; } = "";
        public int EnrollmentYear{ get; set; }
        public string DepartmentName { get; set; } = "";
    }
}
