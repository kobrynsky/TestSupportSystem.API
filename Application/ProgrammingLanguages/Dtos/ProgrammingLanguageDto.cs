using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ProgrammingLanguages.Dtos
{
    public class ProgrammingLanguageDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
    }
}
