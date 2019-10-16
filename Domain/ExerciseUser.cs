using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class ExerciseUser
    {
        public Guid ExerciseId { get; set; }
        public Exercise Exercise { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
