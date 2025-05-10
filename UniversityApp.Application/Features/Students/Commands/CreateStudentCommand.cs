// File: UniversityApp.Application/Features/Students/Commands/CreateStudentCommand.cs
using MediatR;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;

namespace UniversityApp.Application.Features.Students.Commands
{
    public class CreateStudentCommand : IRequest<Result<StudentDto>>
    {
        public CreateStudentDto Student { get; set; }
    }
}
