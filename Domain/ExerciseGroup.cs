using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class ExerciseGroup
    {
        public Guid ExerciseId { get; set; }
        public Exercise Exercise { get; set; }

        public Guid GroupId { get; set; }
        public Group Group { get; set; }
    }
}
