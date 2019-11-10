using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class ExerciseResult
    {
        public Guid Id { get; set; }

        public string StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual ApplicationUser Student { get; set; }
        public string Code { get; set; }

        public virtual IList<CorrectnessTestResult> CorrectnessTestResults { get; set; }

    }
}
