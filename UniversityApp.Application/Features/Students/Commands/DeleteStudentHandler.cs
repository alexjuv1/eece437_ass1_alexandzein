using MediatR;
using UniversityApp.Application.Common;
using UniversityApp.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityApp.Application.Features.Students.Commands
{
    public class DeleteStudentHandler : IRequestHandler<DeleteStudentCommand, Result<bool>>
    {
        private readonly IStudentRepository _repo;

        public DeleteStudentHandler(IStudentRepository repo) => _repo = repo;

        public async Task<Result<bool>> Handle(DeleteStudentCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(request.StudentId);
            if (entity == null)
                return Result<bool>.Fail($"Student {request.StudentId} not found");

            await _repo.DeleteAsync(entity);
            return Result<bool>.Ok(true);
        }
    }
}
