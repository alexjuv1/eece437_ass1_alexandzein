using MediatR;
using UniversityApp.Application.Common;
using UniversityApp.Core.Interfaces;

namespace UniversityApp.Application.Features.Professors.Commands
{
    public class DeleteProfessorCommand : IRequest<Result>
    {
        public int Id { get; set; }

        public DeleteProfessorCommand(int id)
        {
            Id = id;
        }
    }

    public class DeleteProfessorHandler : IRequestHandler<DeleteProfessorCommand, Result>
    {
        private readonly IProfessorRepository _professorRepository;

        public DeleteProfessorHandler(IProfessorRepository professorRepository)
        {
            _professorRepository = professorRepository;
        }

        public async Task<Result> Handle(DeleteProfessorCommand request, CancellationToken cancellationToken)
        {
            var professor = await _professorRepository.GetByIdAsync(request.Id);
            if (professor == null)
                return Result.Failure("Professor not found.");

            await _professorRepository.DeleteAsync(professor);
            return Result.Success();
        }
    }
}
