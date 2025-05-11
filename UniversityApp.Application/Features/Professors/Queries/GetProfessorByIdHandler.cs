// Path: UniversityApp.Application/Features/Professors/Queries/GetProfessorByIdHandler.cs

using MediatR;
using AutoMapper;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;
using UniversityApp.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityApp.Application.Features.Professors.Queries
{
    public class GetProfessorByIdHandler : IRequestHandler<GetProfessorByIdQuery, Result<ProfessorDto>>
    {
        private readonly IProfessorRepository _repo;
        private readonly IMapper              _mapper;

        public GetProfessorByIdHandler(IProfessorRepository repo, IMapper mapper)
        {
            _repo   = repo;
            _mapper = mapper;
        }

        public async Task<Result<ProfessorDto>> Handle(GetProfessorByIdQuery request, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(request.ProfessorId);
            if (entity == null)
                return Result<ProfessorDto>.Fail($"Professor {request.ProfessorId} not found");

            var dto = _mapper.Map<ProfessorDto>(entity);
            return Result<ProfessorDto>.Ok(dto);
        }
    }
}
