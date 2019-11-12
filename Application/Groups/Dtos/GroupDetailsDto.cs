using Application.Courses.Dtos;
using Application.Exercises.Dtos;
using System;
using System.Collections.Generic;

namespace Application.Groups.Dtos
{
    public class GroupDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CourseDto Course { get; set; }
        public List<GroupMemberDto> Members { get; set; }
        public List<ExerciseGroupDetails> Exercises { get; set; }
    }
}
