using MediatR;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;
using UniversityApp.Core.Entities;
using UniversityApp.Core.Interfaces;

namespace UniversityApp.Application.Features.Students.Commands
{
    public class CreateStudentCommand : IRequest<Result<int>>
    {
        public CreateStudentDto Dto { get; set; }
        public CreateStudentCommand(CreateStudentDto dto) => Dto = dto;
    }

    public class CreateStudentHandler : IRequestHandler<CreateStudentCommand, Result<int>>
    {
        private readonly IStudentRepository _repo;

        public CreateStudentHandler(IStudentRepository repo) => _repo = repo;

        public async Task<Result<int>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = new Student
            {
                FullName = request.Dto.FullName,
                Email = request.Dto.Email,
                EnrollmentDate = request.Dto.EnrollmentDate
            };

            await _repo.AddAsync(student);
            return Result<int>.Success(student.Id);
        }
    }
}
