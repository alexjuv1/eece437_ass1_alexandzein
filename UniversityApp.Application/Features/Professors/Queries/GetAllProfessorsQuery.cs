using MediatR;
using UniversityApp.Application.DTOs;
using UniversityApp.Application.Common;
using UniversityApp.Core.Interfaces;

namespace UniversityApp.Application.Features.Professors.Queries
{
    public class GetAllProfessorsQuery : IRequest<Result<List<ProfessorDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetAllProfessorsHandler : IRequestHandler<GetAllProfessorsQuery, Result<List<ProfessorDto>>>
    {
        private readonly IProfessorRepository _professorRepository;

        public GetAllProfessorsHandler(IProfessorRepository professorRepository)
        {
            _professorRepository = professorRepository;
        }

        public async Task<Result<List<ProfessorDto>>> Handle(GetAllProfessorsQuery request, CancellationToken cancellationToken)
        {
            var professors = await _professorRepository.GetAllAsync();

            var paged = professors
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var dtoList = paged.Select(p => new ProfessorDto
            {
                Id = p.Id,
                FullName = p.FullName,
                Department = p.Department,
                Email = p.Email
            }).ToList();

            return Result<List<ProfessorDto>>.Success(dtoList);
        }
    }
}
