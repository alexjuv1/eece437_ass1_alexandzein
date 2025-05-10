using MediatR;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;
using UniversityApp.Core.Interfaces;

namespace UniversityApp.Application.Features.Students.Queries
{
    public class GetStudentByIdQuery : IRequest<Result<StudentDto>>
    {
        public int Id { get; set; }
        public GetStudentByIdQuery(int id) => Id = id;
    }

    public class GetStudentByIdHandler : IRequestHandler<GetStudentByIdQuery, Result<StudentDto>>
    {
        private readonly IStudentRepository _repo;

        public GetStudentByIdHandler(IStudentRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<StudentDto>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _repo.GetByIdAsync(request.Id);
            if (student == null) return Result<StudentDto>.Failure("Student not found.");

            var dto = new StudentDto
            {
                Id = student.Id,
                FullName = student.FullName,
                Email = student.Email,
                EnrollmentDate = student.EnrollmentDate
            };

            return Result<StudentDto>.Success(dto);
        }
    }
}
