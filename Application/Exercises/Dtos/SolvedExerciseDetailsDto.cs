using Application.Courses.Dtos;
using System;
using System.Collections.Generic;

namespace Application.Exercises.Dtos
{
    public class SolvedExerciseDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Code { get; set; }
        public CourseDto Course { get; set; }
        public string ProgrammingLanguage { get; set; }
        public List<CorrectnessTestResultDto> CorrectnessTestsResults { get; set; }
    }
}
