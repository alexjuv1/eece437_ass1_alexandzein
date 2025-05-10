using MediatR;
using UniversityApp.Application.DTOs;
using UniversityApp.Application.Common;
using UniversityApp.Core.Interfaces;

namespace UniversityApp.Application.Features.Courses.Queries
{
    public class GetCourseByIdQuery : IRequest<Result<CourseDto>>
    {
        public int Id { get; set; }

        public GetCourseByIdQuery(int id)
        {
            Id = id;
        }
    }

    public class GetCourseByIdHandler : IRequestHandler<GetCourseByIdQuery, Result<CourseDto>>
    {
        private readonly ICourseRepository _repo;

        public GetCourseByIdHandler(ICourseRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<CourseDto>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var course = await _repo.GetByIdAsync(request.Id);
            if (course == null)
                return Result<CourseDto>.Failure("Course not found.");

            var dto = new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                Credits = course.Credits
            };

            return Result<CourseDto>.Success(dto);
        }
    }
}
