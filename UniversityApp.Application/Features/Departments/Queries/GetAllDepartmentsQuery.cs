using MediatR;
using UniversityApp.Application.DTOs;
using UniversityApp.Application.Common;
using UniversityApp.Core.Interfaces;

namespace UniversityApp.Application.Features.Departments.Queries
{
    public class GetAllDepartmentsQuery : IRequest<Result<List<DepartmentDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetAllDepartmentsHandler : IRequestHandler<GetAllDepartmentsQuery, Result<List<DepartmentDto>>>
    {
        private readonly IDepartmentRepository _repo;

        public GetAllDepartmentsHandler(IDepartmentRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<List<DepartmentDto>>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var departments = await _repo.GetAllAsync();

            var paged = departments
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var dtoList = paged.Select(d => new DepartmentDto
            {
                Id = d.Id,
                Name = d.Name,
                Location = d.Location
            }).ToList();

            return Result<List<DepartmentDto>>.Success(dtoList);
        }
    }
}
