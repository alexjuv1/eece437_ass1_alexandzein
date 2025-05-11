using MediatR;
using AutoMapper;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;
using UniversityApp.Core.Entities;
using UniversityApp.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityApp.Application.Features.Departments.Commands
{
    public class CreateDepartmentHandler : IRequestHandler<CreateDepartmentCommand, Result<DepartmentDto>>
    {
        private readonly IDepartmentRepository _repo;
        private readonly IMapper               _mapper;

        public CreateDepartmentHandler(IDepartmentRepository repo, IMapper mapper)
        {
            _repo   = repo;
            _mapper = mapper;
        }

        public async Task<Result<DepartmentDto>> Handle(CreateDepartmentCommand request, CancellationToken ct)
        {
            var entity = _mapper.Map<Department>(request.Department);
            await _repo.AddAsync(entity);
            var dto = _mapper.Map<DepartmentDto>(entity);
            return Result<DepartmentDto>.Ok(dto);
        }
    }
}
