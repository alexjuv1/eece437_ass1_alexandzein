using MediatR;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;
using UniversityApp.Core.Entities;
using UniversityApp.Core.Interfaces;

namespace UniversityApp.Application.Features.Courses.Commands
{
    public class CreateCourseCommand : IRequest<Result<int>>
    {
        public CreateCourseDto Dto { get; set; }
        public CreateCourseCommand(CreateCourseDto dto) => Dto = dto;
    }

    public class CreateCourseHandler : IRequestHandler<CreateCourseCommand, Result<int>>
    {
        private readonly ICourseRepository _repo;

        public CreateCourseHandler(ICourseRepository repo) => _repo = repo;

        public async Task<Result<int>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = new Course
            {
                Title = request.Dto.Title,
                Credits = request.Dto.Credits
            };

            await _repo.AddAsync(course);
            return Result<int>.Success(course.Id);
        }
    }
}
