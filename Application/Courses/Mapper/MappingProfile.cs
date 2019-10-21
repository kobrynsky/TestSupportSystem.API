using Application.Courses.Dtos;
using AutoMapper;
using Domain;

namespace Application.Courses.Mapper
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseDto>();
        }
    }
}
