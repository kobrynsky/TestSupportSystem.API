using Application.Exercises.Dtos;
using AutoMapper;
using Domain;
using System.Linq;

namespace Application.Exercises.Mapper
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Exercise, ExerciseDto>();
            CreateMap<Exercise, ExerciseDetailsDto>();
            CreateMap<CorrectnessTest, CorrectnessTestDto>()
                .ForMember(dest => dest.Inputs, opt => opt.MapFrom(src => src.Inputs.Select(x => x.Content)))
                .ForMember(dest => dest.Outputs, opt => opt.MapFrom(src => src.Outputs.Select(x => x.Content)));
            CreateMap<CorrectnessTestResult, CorrectnessTestResultDto>();
            CreateMap<Exercise, ExerciseGroupDetails>();
        }
    }
}
