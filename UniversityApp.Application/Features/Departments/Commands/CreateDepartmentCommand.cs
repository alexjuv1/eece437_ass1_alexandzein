using MediatR;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;

namespace UniversityApp.Application.Features.Departments.Commands
{
    public class CreateDepartmentCommand : IRequest<Result<DepartmentDto>>
    {
        public CreateDepartmentDto Department { get; set; }
    }
}
