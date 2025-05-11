using MediatR;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;

namespace UniversityApp.Application.Features.Students.Commands
{
    public class UpdateStudentCommand : IRequest<Result<StudentDto>>
    {
        public int StudentId        { get; set; }
        public CreateStudentDto UpdatedData { get; set; }
    }
}
