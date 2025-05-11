// Path: UniversityApp.Application/Features/Departments/Queries/GetDepartmentByIdQuery.cs

using MediatR;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;

namespace UniversityApp.Application.Features.Departments.Queries
{
    public class GetDepartmentByIdQuery : IRequest<Result<DepartmentDto>>
    {
        public int DepartmentId { get; set; }
    }
}
