// Path: UniversityApp.Application/Features/Departments/Queries/GetDepartmentByIdHandler.cs

using MediatR;
using AutoMapper;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;
using UniversityApp.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityApp.Application.Features.Departments.Queries
{
    public class GetDepartmentByIdHandler : IRequestHandler<GetDepartmentByIdQuery, Result<DepartmentDto>>
    {
        private readonly IDepartmentRepository _repo;
        private readonly IMapper               _mapper;

        public GetDepartmentByIdHandler(IDepartmentRepository repo, IMapper mapper)
        {
            _repo   = repo;
            _mapper = mapper;
        }

        public async Task<Result<DepartmentDto>> Handle(GetDepartmentByIdQuery request, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(request.DepartmentId);
            if (entity == null)
                return Result<DepartmentDto>.Fail($"Department {request.DepartmentId} not found");

            var dto = _mapper.Map<DepartmentDto>(entity);
            return Result<DepartmentDto>.Ok(dto);
        }
    }
}
