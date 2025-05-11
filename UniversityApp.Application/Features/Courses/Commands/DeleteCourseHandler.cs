using MediatR;
using UniversityApp.Application.Common;
using UniversityApp.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityApp.Application.Features.Courses.Commands
{
    public class DeleteCourseHandler : IRequestHandler<DeleteCourseCommand, Result<bool>>
    {
        private readonly ICourseRepository _repo;
        public DeleteCourseHandler(ICourseRepository repo) => _repo = repo;

        public async Task<Result<bool>> Handle(DeleteCourseCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(request.CourseId);
            if (entity == null)
                return Result<bool>.Fail($"Course {request.CourseId} not found");

            await _repo.DeleteAsync(entity);
            return Result<bool>.Ok(true);
        }
    }
}
