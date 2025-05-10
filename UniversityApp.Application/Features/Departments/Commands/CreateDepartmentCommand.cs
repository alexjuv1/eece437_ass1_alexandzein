using MediatR;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;
using UniversityApp.Core.Entities;
using UniversityApp.Core.Interfaces;

namespace UniversityApp.Application.Features.Departments.Commands
{
    public class CreateDepartmentCommand : IRequest<Result<int>>
    {
        public CreateDepartmentDto Dto { get; set; }
        public CreateDepartmentCommand(CreateDepartmentDto dto) => Dto = dto;
    }

    public class CreateDepartmentHandler : IRequestHandler<CreateDepartmentCommand, Result<int>>
    {
        private readonly IDepartmentRepository _repo;

        public CreateDepartmentHandler(IDepartmentRepository repo) => _repo = repo;

        public async Task<Result<int>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var dept = new Department
            {
                Name = request.Dto.Name,
                Location = request.Dto.Location
            };

            await _repo.AddAsync(dept);
            return Result<int>.Success(dept.Id);
        }
    }
}
