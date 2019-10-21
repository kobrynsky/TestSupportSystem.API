using System;
using System.Collections.Generic;
using System.Text;
using Application.Courses.Dtos;

namespace Application.Groups.Dtos
{
    public class GroupDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CourseDto Course { get; set; }
        public List<GroupMemberDto> Members { get; set; }
    }
}
