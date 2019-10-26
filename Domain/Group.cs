using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Group
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual IList<UserGroup> UserGroups { get; set; }

        public virtual IList<ExerciseGroup> ExerciseGroups { get; set; }

        public Guid CourseId { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
    }
}
