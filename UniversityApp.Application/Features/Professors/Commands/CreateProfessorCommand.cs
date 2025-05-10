using MediatR;
using UniversityApp.Application.DTOs;
using UniversityApp.Application.Common;
using UniversityApp.Core.Entities;
using UniversityApp.Core.Interfaces;

namespace UniversityApp.Application.Features.Professors.Commands
{
    public class CreateProfessorCommand : IRequest<Result<int>>
    {
        public CreateProfessorDto ProfessorDto { get; set; }

        public CreateProfessorCommand(CreateProfessorDto dto)
        {
            ProfessorDto = dto;
        }
    }

    public class CreateProfessorHandler : IRequestHandler<CreateProfessorCommand, Result<int>>
    {
        private readonly IProfessorRepository _professorRepository;

        public CreateProfessorHandler(IProfessorRepository professorRepository)
        {
            _professorRepository = professorRepository;
        }

        public async Task<Result<int>> Handle(CreateProfessorCommand request, CancellationToken cancellationToken)
        {
            var professor = new Professor
            {
                FullName = request.ProfessorDto.FullName,
                Department = request.ProfessorDto.Department,
                Email = request.ProfessorDto.Email
            };

            await _professorRepository.AddAsync(professor);
            return Result<int>.Success(professor.Id);
        }
    }
}
