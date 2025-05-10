using MediatR;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;
using UniversityApp.Core.Interfaces;

namespace UniversityApp.Application.Features.Courses.Commands
{
    public class UpdateCourseCommand : IRequest<Result>
    {
        public UpdateCourseDto Dto { get; set; }
        public UpdateCourseCommand(UpdateCourseDto dto) => Dto = dto;
    }

    public class UpdateCourseHandler : IRequestHandler<UpdateCourseCommand, Result>
    {
        private readonly ICourseRepository _repo;

        public UpdateCourseHandler(ICourseRepository repo) => _repo = repo;

        public async Task<Result> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repo.GetByIdAsync(request.Dto.Id);
            if (existing == null) return Result.Failure("Course not found.");

            existing.Title = request.Dto.Title;
            existing.Credits = request.Dto.Credits;

            await _repo.UpdateAsync(existing);
            return Result.Success();
        }
    }
}
