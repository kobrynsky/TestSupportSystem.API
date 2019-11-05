using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class CorrectnessTest
    {
        public Guid Id { get; set; }

        public Guid ExerciseId { get; set; }
        [ForeignKey("ExerciseId")]
        public virtual Exercise Exercise { get; set; }

        public IList<CorrectnessTestInput> Inputs { get; set; }
        public IList<CorrectnessTestOutput> Outputs { get; set; }   
    }
}
