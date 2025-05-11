// Path: UniversityApp.Application/Features/Departments/Queries/GetAllDepartmentsQuery.cs

using MediatR;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;
using System.Collections.Generic;

namespace UniversityApp.Application.Features.Departments.Queries
{
    public class GetAllDepartmentsQuery : IRequest<Result<List<DepartmentDto>>> { }
}
