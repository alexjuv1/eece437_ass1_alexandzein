using MediatR;
using UniversityApp.Application.Common;

namespace UniversityApp.Application.Features.Professors.Commands
{
    public class DeleteProfessorCommand : IRequest<Result<bool>>
    {
        public int ProfessorId { get; set; }
    }
}
