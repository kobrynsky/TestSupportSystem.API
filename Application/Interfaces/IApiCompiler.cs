using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IApiCompiler
    {
        Task<string> SendSubmission(string code, string programmingLanguage, string[] inputs, string[] outputs);
    }
}
