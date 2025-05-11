using MediatR;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;

namespace UniversityApp.Application.Features.Courses.Commands
{
    public class UpdateCourseCommand : IRequest<Result<CourseDto>>
    {
        public int CourseId             { get; set; }
        public CreateCourseDto Updated  { get; set; }
    }
}
