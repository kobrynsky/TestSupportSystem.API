using Application.Exercises.Models;
using Application.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Compiler
{
    public class ApiCompiler : IApiCompiler
    {
        private readonly HttpClient _httpClient;

        public ApiCompiler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<SubmissionResponse> SendSubmission(Submission submission)
        {
            SetUpHeaders();
            var stringContent = new StringContent(JsonConvert.SerializeObject(submission), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("submissions/?base64_encoded=false&wait=true", stringContent);
            var contents = await response.Content.ReadAsStringAsync();
            var parsedResponse = JToken.Parse(contents).ToObject<SubmissionResponse>();
            return parsedResponse;
        }

        private void SetUpHeaders()
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();

            var cacheHeader = new CacheControlHeaderValue { NoCache = true };

            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.CacheControl = cacheHeader;
        }
    }
}
