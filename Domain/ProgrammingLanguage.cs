using System;
using System.Collections.Generic;

namespace Domain
{
    public class ProgrammingLanguage
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string CompilerUrl { get; set; }
        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}
