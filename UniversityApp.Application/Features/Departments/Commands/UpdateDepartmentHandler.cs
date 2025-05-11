using MediatR;
using AutoMapper;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;
using UniversityApp.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityApp.Application.Features.Departments.Commands
{
    public class UpdateDepartmentHandler : IRequestHandler<UpdateDepartmentCommand, Result<DepartmentDto>>
    {
        private readonly IDepartmentRepository _repo;
        private readonly IMapper               _mapper;

        public UpdateDepartmentHandler(IDepartmentRepository repo, IMapper mapper)
        {
            _repo   = repo;
            _mapper = mapper;
        }

        public async Task<Result<DepartmentDto>> Handle(UpdateDepartmentCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(request.DepartmentId);
            if (entity == null)
                return Result<DepartmentDto>.Fail($"Department {request.DepartmentId} not found");

            _mapper.Map(request.Updated, entity);
            await _repo.UpdateAsync(entity);

            return Result<DepartmentDto>.Ok(_mapper.Map<DepartmentDto>(entity));
        }
    }
}
