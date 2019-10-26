using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class CourseMainLecturer
    {
        public Guid CourseId { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }

        public string MainLecturerId { get; set; }
        [ForeignKey("MainLecturerId")]
        public virtual ApplicationUser MainLecturer { get; set; }
    }
}
