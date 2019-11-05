using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class CorrectnessTestInput
    {
        public Guid Id { get; set; }
        public string Content { get; set; }

        public Guid CorrectnessTestId { get; set; }

        [ForeignKey("CorrectnessTestId")]
        public virtual CorrectnessTest CorrectnessTest { get; set; }
    }
}
