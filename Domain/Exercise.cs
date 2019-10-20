using System;
using System.Collections.Generic;

namespace Domain
{
    public class Exercise
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string InitialCode { get; set; }
        public Guid CourseId { get; set; }
        public virtual Course Course { get; set; }
        public Guid ProgrammingLanguageId { get; set; }
        public virtual ProgrammingLanguage ProgrammingLanguage { get; set; }
        public virtual IList<ExerciseGroup> ExerciseGroups { get; set; }
        public virtual IList<ExerciseUser> ExerciseUsers { get; set; }

    }
}
