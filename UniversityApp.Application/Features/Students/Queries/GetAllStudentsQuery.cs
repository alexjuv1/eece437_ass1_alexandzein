using MediatR;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;
using System.Collections.Generic;

namespace UniversityApp.Application.Features.Students.Queries
{
    public class GetAllStudentsQuery : IRequest<Result<List<StudentDto>>> { }
}
