using MediatR;
using UniversityApp.Application.Common;

namespace UniversityApp.Application.Features.Courses.Commands
{
    public class DeleteCourseCommand : IRequest<Result<bool>>
    {
        public int CourseId { get; set; }
    }
}
