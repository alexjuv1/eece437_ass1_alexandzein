using MediatR;
using AutoMapper;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;
using UniversityApp.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityApp.Application.Features.Students.Queries
{
    public class GetStudentByIdHandler : IRequestHandler<GetStudentByIdQuery, Result<StudentDto>>
    {
        private readonly IStudentRepository _repo;
        private readonly IMapper            _mapper;

        public GetStudentByIdHandler(IStudentRepository repo, IMapper mapper)
        {
            _repo   = repo;
            _mapper = mapper;
        }

        public async Task<Result<StudentDto>> Handle(GetStudentByIdQuery request, CancellationToken ct)
        {
            var s = await _repo.GetByIdAsync(request.StudentId);
            if (s == null)
                return Result<StudentDto>.Fail($"Student {request.StudentId} not found");
            return Result<StudentDto>.Ok(_mapper.Map<StudentDto>(s));
        }
    }
}
