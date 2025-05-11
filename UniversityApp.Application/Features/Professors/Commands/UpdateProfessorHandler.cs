using MediatR;
using AutoMapper;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;
using UniversityApp.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityApp.Application.Features.Professors.Commands
{
    public class UpdateProfessorHandler : IRequestHandler<UpdateProfessorCommand, Result<ProfessorDto>>
    {
        private readonly IProfessorRepository _repo;
        private readonly IMapper              _mapper;

        public UpdateProfessorHandler(IProfessorRepository repo, IMapper mapper)
        {
            _repo   = repo;
            _mapper = mapper;
        }

        public async Task<Result<ProfessorDto>> Handle(UpdateProfessorCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(request.ProfessorId);
            if (entity == null)
                return Result<ProfessorDto>.Fail($"Professor {request.ProfessorId} not found");

            _mapper.Map(request.Updated, entity);
            await _repo.UpdateAsync(entity);

            return Result<ProfessorDto>.Ok(_mapper.Map<ProfessorDto>(entity));
        }
    }
}
