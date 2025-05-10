using MediatR;
using UniversityApp.Application.DTOs;
using UniversityApp.Application.Common;
using UniversityApp.Core.Interfaces;

namespace UniversityApp.Application.Features.Departments.Queries
{
    public class GetDepartmentByIdQuery : IRequest<Result<DepartmentDto>>
    {
        public int Id { get; set; }

        public GetDepartmentByIdQuery(int id)
        {
            Id = id;
        }
    }

    public class GetDepartmentByIdHandler : IRequestHandler<GetDepartmentByIdQuery, Result<DepartmentDto>>
    {
        private readonly IDepartmentRepository _repo;

        public GetDepartmentByIdHandler(IDepartmentRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<DepartmentDto>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var dept = await _repo.GetByIdAsync(request.Id);
            if (dept == null)
                return Result<DepartmentDto>.Failure("Department not found.");

            var dto = new DepartmentDto
            {
                Id = dept.Id,
                Name = dept.Name,
                Location = dept.Location
            };

            return Result<DepartmentDto>.Success(dto);
        }
    }
}
