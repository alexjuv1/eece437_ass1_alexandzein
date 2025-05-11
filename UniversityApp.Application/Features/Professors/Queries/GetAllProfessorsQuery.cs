// Path: UniversityApp.Application/Features/Professors/Queries/GetAllProfessorsQuery.cs

using MediatR;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;
using System.Collections.Generic;

namespace UniversityApp.Application.Features.Professors.Queries
{
    public class GetAllProfessorsQuery : IRequest<Result<List<ProfessorDto>>> { }
}
