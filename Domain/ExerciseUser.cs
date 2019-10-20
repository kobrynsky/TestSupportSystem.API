using System;

namespace Domain
{
    public class ExerciseUser
    {
        public Guid ExerciseId { get; set; }
        public virtual Exercise Exercise { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
