using System;
using System.Collections.Generic;
using Application.Courses.Dtos;

namespace Application.Groups.Dtos
{
    public class GroupDto
    {
        public Guid Id{ get; set; }
        public string Name { get; set; }
        public CourseDto Course { get; set; }
        public List<GroupMemberDto> Members { get; set; }
    }
}
