using MediatR;
using UniversityApp.Application.Common;
using UniversityApp.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityApp.Application.Features.Departments.Commands
{
    public class DeleteDepartmentHandler : IRequestHandler<DeleteDepartmentCommand, Result<bool>>
    {
        private readonly IDepartmentRepository _repo;

        public DeleteDepartmentHandler(IDepartmentRepository repo) => _repo = repo;

        public async Task<Result<bool>> Handle(DeleteDepartmentCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(request.DepartmentId);
            if (entity == null)
                return Result<bool>.Fail($"Department {request.DepartmentId} not found");

            await _repo.DeleteAsync(entity);
            return Result<bool>.Ok(true);
        }
    }
}
