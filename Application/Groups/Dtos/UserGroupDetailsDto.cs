using Application.Courses.Dtos;
using Application.Exercises.Dtos;
using System;
using System.Collections.Generic;

namespace Application.Groups.Dtos
{
    public class UserGroupDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CourseDto Course { get; set; }
        public List<ExerciseDto> Exercises { get; set; }
    }
}
