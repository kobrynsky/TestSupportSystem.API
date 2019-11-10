using Application.Interfaces;
using Infrastructure.Compiler.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Submission = Infrastructure.Compiler.Models.Submission;

namespace Infrastructure.Compiler
{
    public class ApiCompiler : IApiCompiler
    {
        private HttpClient _httpClient;

        public ApiCompiler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> SendSubmission(string code, string programmingLanguage, string[] inputs, string[] outputs)
        {
            await RunAsync();
            var submission = ConvertToSubmission(code, programmingLanguage, inputs, outputs);

            var stringContent = new StringContent(JsonConvert.SerializeObject(submission), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("submissions/?base64_encoded=false&wait=true", stringContent);
            var contents = await response.Content.ReadAsStringAsync();

            var response2 = JToken.Parse(contents).ToObject<SubmissionResponse>();
            return null;
        }

        private Submission ConvertToSubmission(string code, string programmingLanguage, string[] inputs, string[] outputs)
        {
            return new Submission()
            {
                language_id = GetProgrammingLanguageId(programmingLanguage),
                source_code = code,
                stdin = string.Join("\n", inputs),
                stdout = string.Join("\n", outputs),
            };
        }

        private async Task RunAsync()
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();

            var cacheHeader = new CacheControlHeaderValue();
            cacheHeader.NoCache = true;

            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.CacheControl = cacheHeader;
        }

        private int GetProgrammingLanguageId(string programmingLanguage)
        {
            switch (programmingLanguage)
            {
                case "C++":
                    return 11;
                case "C#":
                    return 16;
                case "Java":
                    return 26;
                case "Python":
                    return 34;
                default:
                    return 0;
            }
        }
    }
}
