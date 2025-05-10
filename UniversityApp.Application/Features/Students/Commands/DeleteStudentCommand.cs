using MediatR;
using UniversityApp.Application.Common;
using UniversityApp.Core.Interfaces;

namespace UniversityApp.Application.Features.Students.Commands
{
    public class DeleteStudentCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public DeleteStudentCommand(int id) => Id = id;
    }

    public class DeleteStudentHandler : IRequestHandler<DeleteStudentCommand, Result>
    {
        private readonly IStudentRepository _repo;

        public DeleteStudentHandler(IStudentRepository repo) => _repo = repo;

        public async Task<Result> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _repo.GetByIdAsync(request.Id);
            if (student == null) return Result.Failure("Student not found.");

            await _repo.DeleteAsync(student);
            return Result.Success();
        }
    }
}
