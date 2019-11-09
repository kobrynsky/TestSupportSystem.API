using Application.Courses.Dtos;
using Application.User.Dtos;
using System;

namespace Application.Exercises.Dtos
{
    public class ExerciseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string InitialCode { get; set; }
        public CourseDto Course { get; set; }
        public UserDto Author { get; set; }
    }
}
