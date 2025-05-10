using MediatR;
using UniversityApp.Application.DTOs;
using UniversityApp.Application.Common;
using UniversityApp.Core.Interfaces;

namespace UniversityApp.Application.Features.Professors.Queries
{
    public class GetProfessorByIdQuery : IRequest<Result<ProfessorDto>>
    {
        public int Id { get; set; }

        public GetProfessorByIdQuery(int id)
        {
            Id = id;
        }
    }

    public class GetProfessorByIdHandler : IRequestHandler<GetProfessorByIdQuery, Result<ProfessorDto>>
    {
        private readonly IProfessorRepository _professorRepository;

        public GetProfessorByIdHandler(IProfessorRepository professorRepository)
        {
            _professorRepository = professorRepository;
        }

        public async Task<Result<ProfessorDto>> Handle(GetProfessorByIdQuery request, CancellationToken cancellationToken)
        {
            var professor = await _professorRepository.GetByIdAsync(request.Id);
            if (professor == null)
                return Result<ProfessorDto>.Failure("Professor not found.");

            var dto = new ProfessorDto
            {
                Id = professor.Id,
                FullName = professor.FullName,
                Department = professor.Department,
                Email = professor.Email
            };

            return Result<ProfessorDto>.Success(dto);
        }
    }
}
