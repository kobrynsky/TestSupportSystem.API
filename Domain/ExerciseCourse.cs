using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class ExerciseCourse
    {
        public Guid ExerciseId { get; set; }
        [ForeignKey("ExerciseId")]
        public virtual Exercise Exercise { get; set; }

        public Guid CourseId { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
    }
}
