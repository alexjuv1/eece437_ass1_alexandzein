using AutoMapper;
using UniversityApp.Application.DTOs;
using UniversityApp.Core.Entities;

namespace UniversityApp.Application.Mappings
{
    public class ProfessorProfile : Profile
    {
        public ProfessorProfile()
        {
            CreateMap<Professor, ProfessorDto>().ReverseMap();
            CreateMap<Professor, CreateProfessorDto>().ReverseMap();
            CreateMap<Professor, UpdateProfessorDto>().ReverseMap();
        }
    }
}
