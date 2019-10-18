using System;

namespace Domain
{
    public class Exercise
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string InitialCode { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        public Guid ProgrammingLanguageId { get; set; }
        public ProgrammingLanguage ProgrammingLanguage { get; set; }

    }
}
