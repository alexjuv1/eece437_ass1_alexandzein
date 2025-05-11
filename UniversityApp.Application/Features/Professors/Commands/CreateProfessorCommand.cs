using MediatR;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;

namespace UniversityApp.Application.Features.Professors.Commands
{
    public class CreateProfessorCommand : IRequest<Result<ProfessorDto>>
    {
        public CreateProfessorDto Professor { get; set; }
    }
}
