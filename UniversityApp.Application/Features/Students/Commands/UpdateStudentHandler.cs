using MediatR;
using AutoMapper;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;
using UniversityApp.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityApp.Application.Features.Students.Commands
{
    public class UpdateStudentHandler : IRequestHandler<UpdateStudentCommand, Result<StudentDto>>
    {
        private readonly IStudentRepository _repo;
        private readonly IMapper            _mapper;

        public UpdateStudentHandler(IStudentRepository repo, IMapper mapper)
        {
            _repo   = repo;
            _mapper = mapper;
        }

        public async Task<Result<StudentDto>> Handle(UpdateStudentCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(request.StudentId);
            if (entity == null)
                return Result<StudentDto>.Fail($"Student {request.StudentId} not found");

            _mapper.Map(request.UpdatedData, entity);
            await _repo.UpdateAsync(entity);

            return Result<StudentDto>.Ok(_mapper.Map<StudentDto>(entity));
        }
    }
}
