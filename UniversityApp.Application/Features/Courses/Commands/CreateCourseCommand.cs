using MediatR;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;

namespace UniversityApp.Application.Features.Courses.Commands
{
    public class CreateCourseCommand : IRequest<Result<CourseDto>>
    {
        public CreateCourseDto Course { get; set; }
    }
}
