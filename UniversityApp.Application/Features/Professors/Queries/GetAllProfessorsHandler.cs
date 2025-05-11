// Path: UniversityApp.Application/Features/Professors/Queries/GetAllProfessorsHandler.cs

using MediatR;
using AutoMapper;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;
using UniversityApp.Core.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityApp.Application.Features.Professors.Queries
{
    public class GetAllProfessorsHandler : IRequestHandler<GetAllProfessorsQuery, Result<List<ProfessorDto>>>
    {
        private readonly IProfessorRepository _repo;
        private readonly IMapper              _mapper;

        public GetAllProfessorsHandler(IProfessorRepository repo, IMapper mapper)
        {
            _repo   = repo;
            _mapper = mapper;
        }

        public async Task<Result<List<ProfessorDto>>> Handle(GetAllProfessorsQuery request, CancellationToken ct)
        {
            var list = await _repo.GetAllAsync();
            var dtos = _mapper.Map<List<ProfessorDto>>(list);
            return Result<List<ProfessorDto>>.Ok(dtos);
        }
    }
}
