using Application.Courses.Dtos;
using Application.User.Dtos;
using System;
using System.Collections.Generic;

namespace Application.Exercises.Dtos
{
    public class ExerciseDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string InitialCode { get; set; }
        public string ProgrammingLanguage { get; set; }
        public CourseDto Course { get; set; }
        public UserDto Author { get; set; }
        public List<CorrectnessTestDto> CorrectnessTests { get; set; }
    }
}
