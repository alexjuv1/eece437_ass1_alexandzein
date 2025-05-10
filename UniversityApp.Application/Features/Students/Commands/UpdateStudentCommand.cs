using MediatR;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;
using UniversityApp.Core.Interfaces;

namespace UniversityApp.Application.Features.Students.Commands
{
    public class UpdateStudentCommand : IRequest<Result>
    {
        public UpdateStudentDto Dto { get; set; }
        public UpdateStudentCommand(UpdateStudentDto dto) => Dto = dto;
    }

    public class UpdateStudentHandler : IRequestHandler<UpdateStudentCommand, Result>
    {
        private readonly IStudentRepository _repo;

        public UpdateStudentHandler(IStudentRepository repo) => _repo = repo;

        public async Task<Result> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repo.GetByIdAsync(request.Dto.Id);
            if (existing == null) return Result.Failure("Student not found.");

            existing.FullName = request.Dto.FullName;
            existing.Email = request.Dto.Email;
            existing.EnrollmentDate = request.Dto.EnrollmentDate;

            await _repo.UpdateAsync(existing);
            return Result.Success();
        }
    }
}
