using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class CorrectnessTestResult
    {
        public Guid Id { get; set; }
        public Guid ExerciseResultId { get; set; }
        [ForeignKey("ExerciseResultId")]
        public virtual ExerciseResult ExerciseResult { get; set; }
        public Guid CorrectnessTestId { get; set; }
        [ForeignKey("CorrectnessTestId")]
        public virtual CorrectnessTest CorrectnessTest { get; set; }
        public string Time { get; set; }
        public int Memory { get; set; }
        public string CompileOutput { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public string Status { get; set; }
    }
}
