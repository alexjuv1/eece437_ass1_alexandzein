// Path: UniversityApp.Application/Features/Professors/Queries/GetProfessorByIdQuery.cs

using MediatR;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;

namespace UniversityApp.Application.Features.Professors.Queries
{
    public class GetProfessorByIdQuery : IRequest<Result<ProfessorDto>>
    {
        public int ProfessorId { get; set; }
    }
}
