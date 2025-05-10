using MediatR;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;
using UniversityApp.Core.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityApp.Application.Features.Students.Queries
{
    public class GetAllStudentsHandler : IRequestHandler<GetAllStudentsQuery, Result<List<StudentDto>>>
    {
        private readonly IStudentRepository _repo;
        private readonly IMapper _mapper;

        public GetAllStudentsHandler(IStudentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<List<StudentDto>>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var students = await _repo.GetAllAsync();
            var dtos = _mapper.Map<List<StudentDto>>(students);
            return Result<List<StudentDto>>.Ok(dtos);
        }
    }
}
