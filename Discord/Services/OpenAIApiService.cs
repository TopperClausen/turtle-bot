using System.Net.Http.Headers;
using System.Text;
using Discord.Models;
using Newtonsoft.Json;

namespace Discord.Services;

public class OpenAIApiService
{
    private HttpClient _client = new HttpClient();
    
    public async Task<HttpResponseMessage> PostAsync(string endpoint, string jsonBody)
    {
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Environment.GetEnvironmentVariable("OPENAI_TOKEN"));
        var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
   
        return await _client.PostAsync(endpoint, content);
    }
}