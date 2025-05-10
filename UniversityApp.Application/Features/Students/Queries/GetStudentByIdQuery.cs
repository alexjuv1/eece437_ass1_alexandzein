// File: UniversityApp.Application/Features/Students/Queries/GetStudentByIdQuery.cs
using MediatR;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;

namespace UniversityApp.Application.Features.Students.Queries
{
    public class GetStudentByIdQuery : IRequest<Result<StudentDto>>
    {
        public int StudentId { get; set; }
    }
}
