using MediatR;
using AutoMapper;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;
using UniversityApp.Core.Entities;
using UniversityApp.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityApp.Application.Features.Courses.Commands
{
    public class CreateCourseHandler : IRequestHandler<CreateCourseCommand, Result<CourseDto>>
    {
        private readonly ICourseRepository _repo;
        private readonly IMapper           _mapper;

        public CreateCourseHandler(ICourseRepository repo, IMapper mapper)
        {
            _repo   = repo;
            _mapper = mapper;
        }

        public async Task<Result<CourseDto>> Handle(CreateCourseCommand request, CancellationToken ct)
        {
            var entity = _mapper.Map<Course>(request.Course);
            await _repo.AddAsync(entity);
            var dto = _mapper.Map<CourseDto>(entity);
            return Result<CourseDto>.Ok(dto);
        }
    }
}
