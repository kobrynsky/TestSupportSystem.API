namespace Application.Exercises.Dtos
{
    public class CorrectnessTestResultDto
    {
        public string Time { get; set; }
        public int Memory { get; set; }
        public string CompileOutput { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public string Status { get; set; }
    }
}
