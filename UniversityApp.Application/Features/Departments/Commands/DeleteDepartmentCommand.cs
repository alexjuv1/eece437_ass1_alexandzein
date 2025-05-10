using MediatR;
using UniversityApp.Application.Common;
using UniversityApp.Core.Interfaces;

namespace UniversityApp.Application.Features.Departments.Commands
{
    public class DeleteDepartmentCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public DeleteDepartmentCommand(int id) => Id = id;
    }

    public class DeleteDepartmentHandler : IRequestHandler<DeleteDepartmentCommand, Result>
    {
        private readonly IDepartmentRepository _repo;

        public DeleteDepartmentHandler(IDepartmentRepository repo) => _repo = repo;

        public async Task<Result> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var dept = await _repo.GetByIdAsync(request.Id);
            if (dept == null) return Result.Failure("Department not found.");

            await _repo.DeleteAsync(dept);
            return Result.Success();
        }
    }
}
