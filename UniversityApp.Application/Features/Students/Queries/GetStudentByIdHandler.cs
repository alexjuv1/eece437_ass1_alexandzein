// File: UniversityApp.Application/Features/Students/Queries/GetStudentByIdHandler.cs
using MediatR;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;
using UniversityApp.Core.Interfaces;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityApp.Application.Features.Students.Queries
{
    public class GetStudentByIdHandler : IRequestHandler<GetStudentByIdQuery, Result<StudentDto>>
    {
        private readonly IStudentRepository _repo;
        private readonly IMapper _mapper;

        public GetStudentByIdHandler(IStudentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<StudentDto>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _repo.GetByIdAsync(request.StudentId);
            if (student == null)
                return Result<StudentDto>.Fail($"Student with ID {request.StudentId} not found.");
            var dto = _mapper.Map<StudentDto>(student);
            return Result<StudentDto>.Ok(dto);
        }
    }
}
