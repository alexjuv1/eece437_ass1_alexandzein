using MediatR;
using UniversityApp.Application.DTOs;
using UniversityApp.Application.Common;
using UniversityApp.Core.Entities;
using UniversityApp.Core.Interfaces;

namespace UniversityApp.Application.Features.Professors.Commands
{
    public class UpdateProfessorCommand : IRequest<Result>
    {
        public UpdateProfessorDto ProfessorDto { get; set; }

        public UpdateProfessorCommand(UpdateProfessorDto dto)
        {
            ProfessorDto = dto;
        }
    }

    public class UpdateProfessorHandler : IRequestHandler<UpdateProfessorCommand, Result>
    {
        private readonly IProfessorRepository _professorRepository;

        public UpdateProfessorHandler(IProfessorRepository professorRepository)
        {
            _professorRepository = professorRepository;
        }

        public async Task<Result> Handle(UpdateProfessorCommand request, CancellationToken cancellationToken)
        {
            var professor = await _professorRepository.GetByIdAsync(request.ProfessorDto.Id);
            if (professor == null)
                return Result.Failure("Professor not found.");

            professor.FullName = request.ProfessorDto.FullName;
            professor.Department = request.ProfessorDto.Department;
            professor.Email = request.ProfessorDto.Email;

            await _professorRepository.UpdateAsync(professor);
            return Result.Success();
        }
    }
}
