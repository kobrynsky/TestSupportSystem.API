using System;
using System.Collections.Generic;
using System.Text;
using Application.Exercises.Dtos;
using AutoMapper;
using Domain;

namespace Application.Exercises.Mapper
{
    class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Exercise, ExerciseDto>();
        }
    }
}
