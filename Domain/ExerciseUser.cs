using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class ExerciseUser
    {
        public Guid Id { get; set; }
        
        public Guid ExerciseId { get; set; }
        [ForeignKey("ExerciseId")]
        public virtual Exercise Exercise { get; set; }
        
        public string StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual ApplicationUser Student { get; set; }

        public string LecturerId { get; set; }
        [ForeignKey("LecturerId")]
        public virtual ApplicationUser Lecturer { get; set; }

        public decimal Grade { get; set; }
        public DateTime DateOfAssesment { get; set; }

        public Guid GroupId { get; set; }
        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }

    }
}
