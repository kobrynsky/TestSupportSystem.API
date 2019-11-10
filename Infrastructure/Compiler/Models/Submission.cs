namespace Infrastructure.Compiler.Models
{
    public class Submission
    {
        public string source_code { get; set; }
        public int language_id { get; set; }
        public string stdin { get; set; }
        public string stdout { get; set; }
    }
}
