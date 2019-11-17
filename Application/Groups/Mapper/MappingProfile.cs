using Application.Groups.Dtos;
using AutoMapper;
using Domain;
using System.Linq;

namespace Application.Groups.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, GroupMemberDto>().ReverseMap();
            CreateMap<Group, GroupDto>()
                .ForMember(d => d.Members, o => o.MapFrom(s => s.UserGroups.Select(x => x.User)));
            CreateMap<Group, GroupDetailsDto>()
                .ForMember(d => d.Members, o => o.MapFrom(s => s.UserGroups.Select(x => x.User)))
                .ForMember(d => d.Exercises, o => o.MapFrom(s => s.ExerciseGroups.Select(x => x.Exercise)));
            CreateMap<Group, UserGroupDetailsDto>()
                .ForMember(d => d.Exercises, o => o.MapFrom(s => s.ExerciseGroups.Select(x => x.Exercise)));
        }
    }
}

