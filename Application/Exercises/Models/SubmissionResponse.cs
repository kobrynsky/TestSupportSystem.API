namespace Application.Exercises.Models
{
    public class SubmissionResponse
    {
        public string stdout { get; set; }
        public string time { get; set; }
        public int memory { get; set; }
        public string stderr { get; set; }
        public string token { get; set; }
        public string compile_output { get; set; }
        public string message { get; set; }
        public Status status { get; set; }
    }

    public class Status
    {
        public int id { get; set; }
        public string description { get; set; }
    }

    public static class StatusDescription
    {
        public const string Accepted = "Accepted";
        public const string WrongAnswer = "Wrong Answer";
        public const string TimeLimitExceeded = "Time Limit Exceeded";
        public const string CompilationError = "Compilation Error";
        public const string InternalError = "Internal Error";
    }
}
