using MediatR;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;
using UniversityApp.Core.Interfaces;

namespace UniversityApp.Application.Features.Students.Queries
{
    public class GetAllStudentsQuery : IRequest<Result<List<StudentDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetAllStudentsHandler : IRequestHandler<GetAllStudentsQuery, Result<List<StudentDto>>>
    {
        private readonly IStudentRepository _repo;

        public GetAllStudentsHandler(IStudentRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<List<StudentDto>>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var students = await _repo.GetAllAsync();

            var paged = students
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var dtoList = paged.Select(s => new StudentDto
            {
                Id = s.Id,
                FullName = s.FullName,
                Email = s.Email,
                EnrollmentDate = s.EnrollmentDate
            }).ToList();

            return Result<List<StudentDto>>.Success(dtoList);
        }
    }
}
