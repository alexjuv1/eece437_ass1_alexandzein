using MediatR;
using AutoMapper;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;
using UniversityApp.Core.Entities;
using UniversityApp.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityApp.Application.Features.Professors.Commands
{
    public class CreateProfessorHandler : IRequestHandler<CreateProfessorCommand, Result<ProfessorDto>>
    {
        private readonly IProfessorRepository _repo;
        private readonly IMapper              _mapper;

        public CreateProfessorHandler(IProfessorRepository repo, IMapper mapper)
        {
            _repo   = repo;
            _mapper = mapper;
        }

        public async Task<Result<ProfessorDto>> Handle(CreateProfessorCommand request, CancellationToken ct)
        {
            var entity = _mapper.Map<Professor>(request.Professor);
            await _repo.AddAsync(entity);
            var dto = _mapper.Map<ProfessorDto>(entity);
            return Result<ProfessorDto>.Ok(dto);
        }
    }
}
