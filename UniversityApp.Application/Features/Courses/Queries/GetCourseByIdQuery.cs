// Path: UniversityApp.Application/Features/Courses/Queries/GetCourseByIdQuery.cs

using MediatR;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;

namespace UniversityApp.Application.Features.Courses.Queries
{
    public class GetCourseByIdQuery : IRequest<Result<CourseDto>>
    {
        public int CourseId { get; set; }
    }
}
