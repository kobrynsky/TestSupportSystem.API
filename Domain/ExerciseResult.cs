using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class ExerciseResult
    {
        public Guid Id { get; set; }

        public Guid CorrectnessTestId { get; set; }
        [ForeignKey("CorrectnessTestId")]
        public virtual CorrectnessTest CorrectnessTest { get; set; }

        public string StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual ApplicationUser Student { get; set; }

        public string Time { get; set; }
        public int Memory { get; set; }
        public string CompileOutput { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public string Status { get; set; }
    }
}
