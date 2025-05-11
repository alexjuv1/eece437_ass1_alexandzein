// Path: UniversityApp.Application/Features/Departments/Queries/GetAllDepartmentsHandler.cs

using MediatR;
using AutoMapper;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;
using UniversityApp.Core.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityApp.Application.Features.Departments.Queries
{
    public class GetAllDepartmentsHandler : IRequestHandler<GetAllDepartmentsQuery, Result<List<DepartmentDto>>>
    {
        private readonly IDepartmentRepository _repo;
        private readonly IMapper               _mapper;

        public GetAllDepartmentsHandler(IDepartmentRepository repo, IMapper mapper)
        {
            _repo   = repo;
            _mapper = mapper;
        }

        public async Task<Result<List<DepartmentDto>>> Handle(GetAllDepartmentsQuery request, CancellationToken ct)
        {
            var list = await _repo.GetAllAsync();
            var dtos = _mapper.Map<List<DepartmentDto>>(list);
            return Result<List<DepartmentDto>>.Ok(dtos);
        }
    }
}
