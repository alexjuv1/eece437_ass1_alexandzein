using MediatR;
using AutoMapper;
using UniversityApp.Application.Common;
using UniversityApp.Application.DTOs;
using UniversityApp.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace UniversityApp.Application.Features.Courses.Commands
{
    public class UpdateCourseHandler : IRequestHandler<UpdateCourseCommand, Result<CourseDto>>
    {
        private readonly ICourseRepository _repo;
        private readonly IMapper           _mapper;

        public UpdateCourseHandler(ICourseRepository repo, IMapper mapper)
        {
            _repo   = repo;
            _mapper = mapper;
        }

        public async Task<Result<CourseDto>> Handle(UpdateCourseCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(request.CourseId);
            if (entity == null)
                return Result<CourseDto>.Fail($"Course {request.CourseId} not found");

            _mapper.Map(request.Updated, entity);
            await _repo.UpdateAsync(entity);

            return Result<CourseDto>.Ok(_mapper.Map<CourseDto>(entity));
        }
    }
}
