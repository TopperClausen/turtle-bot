using Discord.Models;
using Newtonsoft.Json;

namespace Discord.Services;

public class DalleService : OpenAIApiService
{
    private String endpoint = "https://api.openai.com/v1/images/generations";
    
    public async Task<String> Ask(String question)
    {
        try
        {
            HttpResponseMessage response = await PostAsync(endpoint, JsonBody(question));

            var body =  await response.Content.ReadAsStringAsync();
            
            if (!response.IsSuccessStatusCode) return $"Request failed: {body}";
            
            return UrlFromBody(body);

        }
        catch (Exception e)
        {
            return e.Message;
        }
    }
    
    private string JsonBody(string question)
    {
        var body = new DalleBody(question);
        return JsonConvert.SerializeObject(body);
    }

    private string UrlFromBody(dynamic body)
    {
        dynamic jsonObject = JsonConvert.DeserializeObject(body);
        string url = jsonObject.data[0].url;
        return url;
    }
}