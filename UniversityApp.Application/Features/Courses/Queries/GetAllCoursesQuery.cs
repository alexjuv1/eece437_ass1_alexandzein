// Path: UniversityApp.Application/Features/Courses/Queries/GetAllCoursesQuery.cs

using MediatR;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;
using System.Collections.Generic;

namespace UniversityApp.Application.Features.Courses.Queries
{
    public class GetAllCoursesQuery : IRequest<Result<List<CourseDto>>> { }
}
