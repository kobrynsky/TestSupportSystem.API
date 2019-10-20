using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Groups.Dtos;
using AutoMapper;
using Domain;

namespace Application.Groups
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
