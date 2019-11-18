using Application.Users.Dtos;
using System;

namespace Application.Exercises.Dtos
{
    public class ExerciseGroupDetails
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ProgrammingLanguage { get; set; }
        public UserDto Author { get; set; }
        public bool Solved { get; set; }
    }
}
