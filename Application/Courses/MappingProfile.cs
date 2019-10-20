using System;
using System.Collections.Generic;
using System.Text;
using Application.Courses.Dtos;
using AutoMapper;
using Domain;

namespace Application.Courses
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseDto>();
        }
    }
}
