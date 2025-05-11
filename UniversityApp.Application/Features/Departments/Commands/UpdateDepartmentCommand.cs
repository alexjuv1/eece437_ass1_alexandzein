using MediatR;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;

namespace UniversityApp.Application.Features.Departments.Commands
{
    public class UpdateDepartmentCommand : IRequest<Result<DepartmentDto>>
    {
        public int DepartmentId           { get; set; }
        public CreateDepartmentDto Updated{ get; set; }
    }
}
