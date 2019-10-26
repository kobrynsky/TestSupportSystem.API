using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class CourseMainLecturer
    {
        public Guid Id { get; set; }
        public string MainLecturerId { get; set; }
        [ForeignKey("MainLecturerId")]
        public virtual ApplicationUser MainLecturer { get; set; }

        public Guid CourseId { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
    }
}
