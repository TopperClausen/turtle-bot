using System.Net.Http.Headers;
using System.Text;
using Discord.Models;
using Newtonsoft.Json;

namespace Discord.Services;

public class DalleService
{
    private String endpoint = "https://api.openai.com/v1/images/generations";
    private String _token = Environment.GetEnvironmentVariable("OPENAI_TOKEN");
    
    public async Task<String> Ask(String question)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        var content = new StringContent(jsonBody(question), Encoding.UTF8, "application/json");

        try
        {
            HttpResponseMessage response = await client.PostAsync(endpoint, content);

            var body =  await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                dynamic jsonObject = JsonConvert.DeserializeObject(body);
                string url = jsonObject.data[0].url;
                return url;
            }
            return $"Request failed: {body}";
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    private string jsonBody(string question)
    {
        var body = new DalleBody(question);
        return JsonConvert.SerializeObject(body);
    }
}