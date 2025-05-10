using MediatR;
using UniversityApp.Application.Common;
using UniversityApp.Core.Interfaces;

namespace UniversityApp.Application.Features.Courses.Commands
{
    public class DeleteCourseCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public DeleteCourseCommand(int id) => Id = id;
    }

    public class DeleteCourseHandler : IRequestHandler<DeleteCourseCommand, Result>
    {
        private readonly ICourseRepository _repo;

        public DeleteCourseHandler(ICourseRepository repo) => _repo = repo;

        public async Task<Result> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _repo.GetByIdAsync(request.Id);
            if (course == null) return Result.Failure("Course not found.");

            await _repo.DeleteAsync(course);
            return Result.Success();
        }
    }
}
