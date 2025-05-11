namespace UniversityApp.Application.DTOs
{
    public class CreateStudentDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
