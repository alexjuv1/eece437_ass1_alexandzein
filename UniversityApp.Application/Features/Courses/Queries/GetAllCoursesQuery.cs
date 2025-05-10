using MediatR;
using UniversityApp.Application.DTOs;
using UniversityApp.Application.Common;
using UniversityApp.Core.Interfaces;

namespace UniversityApp.Application.Features.Courses.Queries
{
    public class GetAllCoursesQuery : IRequest<Result<List<CourseDto>>> { }

    public class GetAllCoursesHandler : IRequestHandler<GetAllCoursesQuery, Result<List<CourseDto>>>
    {
        private readonly ICourseRepository _repo;

        public GetAllCoursesHandler(ICourseRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<List<CourseDto>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            var courses = await _repo.GetAllAsync();
            var dtoList = courses.Select(c => new CourseDto
            {
                Id = c.Id,
                Title = c.Title,
                Credits = c.Credits
            }).ToList();

            return Result<List<CourseDto>>.Success(dtoList);
        }
    }
}
