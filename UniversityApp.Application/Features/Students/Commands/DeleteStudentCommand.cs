using MediatR;
using UniversityApp.Application.Common;

namespace UniversityApp.Application.Features.Students.Commands
{
    public class DeleteStudentCommand : IRequest<Result<bool>>
    {
        public int StudentId { get; set; }
    }
}
