using MediatR;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;

namespace UniversityApp.Application.Features.Professors.Commands
{
    public class UpdateProfessorCommand : IRequest<Result<ProfessorDto>>
    {
        public int ProfessorId           { get; set; }
        public CreateProfessorDto Updated{ get; set; }
    }
}
