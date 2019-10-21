using System.Linq;
using Application.Groups.Dtos;
using AutoMapper;
using Domain;

namespace Application.Groups.Mapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, GroupMemberDto>();
            CreateMap<Group, GroupDto>()
                .ForMember(d => d.Members, o => o.MapFrom(s => s.UserGroups.Select(x => x.User)));
        }
    }
}
