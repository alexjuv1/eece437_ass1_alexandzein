// Path: UniversityApp.Application/Features/Courses/Queries/GetAllCoursesHandler.cs

using MediatR;
using AutoMapper;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;
using UniversityApp.Core.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityApp.Application.Features.Courses.Queries
{
    public class GetAllCoursesHandler : IRequestHandler<GetAllCoursesQuery, Result<List<CourseDto>>>
    {
        private readonly ICourseRepository _repo;
        private readonly IMapper           _mapper;

        public GetAllCoursesHandler(ICourseRepository repo, IMapper mapper)
        {
            _repo   = repo;
            _mapper = mapper;
        }

        public async Task<Result<List<CourseDto>>> Handle(GetAllCoursesQuery request, CancellationToken ct)
        {
            var list = await _repo.GetAllAsync();
            var dtos = _mapper.Map<List<CourseDto>>(list);
            return Result<List<CourseDto>>.Ok(dtos);
        }
    }
}
