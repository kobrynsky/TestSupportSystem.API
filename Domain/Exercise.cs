using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Exercise
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string InitialCode { get; set; }
        public string ProgrammingLanguage { get; set; }
        public Guid CourseId { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
        public virtual IList<ExerciseGroup> ExerciseGroups { get; set; }
        public virtual IList<ExerciseUser> ExerciseUsers { get; set; }
        public virtual IList<ExerciseCourse> ExerciseCourses { get; set; }
        public string AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public virtual ApplicationUser Author { get; set; }
    }
}
