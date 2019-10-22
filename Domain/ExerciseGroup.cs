using System;

namespace Domain
{
    public class ExerciseGroup
    {
        public Guid ExerciseId { get; set; }
        public virtual Exercise Exercise { get; set; }

        public Guid GroupId { get; set; }
        public virtual Group Group { get; set; }
    }
}
