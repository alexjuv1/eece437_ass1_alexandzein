using MediatR;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;
using UniversityApp.Core.Interfaces;

namespace UniversityApp.Application.Features.Departments.Commands
{
    public class UpdateDepartmentCommand : IRequest<Result>
    {
        public UpdateDepartmentDto Dto { get; set; }
        public UpdateDepartmentCommand(UpdateDepartmentDto dto) => Dto = dto;
    }

    public class UpdateDepartmentHandler : IRequestHandler<UpdateDepartmentCommand, Result>
    {
        private readonly IDepartmentRepository _repo;

        public UpdateDepartmentHandler(IDepartmentRepository repo) => _repo = repo;

        public async Task<Result> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repo.GetByIdAsync(request.Dto.Id);
            if (existing == null) return Result.Failure("Department not found.");

            existing.Name = request.Dto.Name;
            existing.Location = request.Dto.Location;

            await _repo.UpdateAsync(existing);
            return Result.Success();
        }
    }
}
