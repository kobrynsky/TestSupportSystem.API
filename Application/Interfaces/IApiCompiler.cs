using Application.Exercises.Models;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IApiCompiler
    {
        Task<SubmissionResponse> SendSubmission(Submission submission);
    }
}
