// File: UniversityApp.Application/Features/Students/Commands/CreateStudentHandler.cs
using MediatR;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;
using UniversityApp.Core.Entities;
using UniversityApp.Core.Interfaces;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityApp.Application.Features.Students.Commands
{
    public class CreateStudentHandler : IRequestHandler<CreateStudentCommand, Result<StudentDto>>
    {
        private readonly IStudentRepository _repo;
        private readonly IMapper _mapper;

        public CreateStudentHandler(IStudentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<StudentDto>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Student>(request.Student);
            await _repo.AddAsync(entity);
            var dto = _mapper.Map<StudentDto>(entity);
            return Result<StudentDto>.Ok(dto);
        }
    }
}
