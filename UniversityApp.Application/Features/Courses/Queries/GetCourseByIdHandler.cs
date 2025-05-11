// Path: UniversityApp.Application/Features/Courses/Queries/GetCourseByIdHandler.cs

using MediatR;
using AutoMapper;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;
using UniversityApp.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityApp.Application.Features.Courses.Queries
{
    public class GetCourseByIdHandler : IRequestHandler<GetCourseByIdQuery, Result<CourseDto>>
    {
        private readonly ICourseRepository _repo;
        private readonly IMapper           _mapper;

        public GetCourseByIdHandler(ICourseRepository repo, IMapper mapper)
        {
            _repo   = repo;
            _mapper = mapper;
        }

        public async Task<Result<CourseDto>> Handle(GetCourseByIdQuery request, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(request.CourseId);
            if (entity == null)
                return Result<CourseDto>.Fail($"Course {request.CourseId} not found");

            var dto = _mapper.Map<CourseDto>(entity);
            return Result<CourseDto>.Ok(dto);
        }
    }
}
