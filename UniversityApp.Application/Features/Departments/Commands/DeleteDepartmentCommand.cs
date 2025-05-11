using MediatR;
using UniversityApp.Application.Common;

namespace UniversityApp.Application.Features.Departments.Commands
{
    public class DeleteDepartmentCommand : IRequest<Result<bool>>
    {
        public int DepartmentId { get; set; }
    }
}
