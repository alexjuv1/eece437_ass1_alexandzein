using MediatR;
using UniversityApp.Application.Common;
using UniversityApp.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityApp.Application.Features.Professors.Commands
{
    public class DeleteProfessorHandler : IRequestHandler<DeleteProfessorCommand, Result<bool>>
    {
        private readonly IProfessorRepository _repo;

        public DeleteProfessorHandler(IProfessorRepository repo) => _repo = repo;

        public async Task<Result<bool>> Handle(DeleteProfessorCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(request.ProfessorId);
            if (entity == null)
                return Result<bool>.Fail($"Professor {request.ProfessorId} not found");

            await _repo.DeleteAsync(entity);
            return Result<bool>.Ok(true);
        }
    }
}
