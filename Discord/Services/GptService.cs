using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace Discord.Services;
using Discord.Models.GPT;
using System.Net.Http;
public class GptService : OpenAIApiService
{
    private String endpoint = "https://api.openai.com/v1/chat/completions";
    private String _token = Environment.GetEnvironmentVariable("OPENAI_TOKEN");
    public async Task<String> Ask(String question)
    {
        try
        {
            HttpResponseMessage response = await PostAsync(endpoint, jsonBody(question));

            var body =  await response.Content.ReadAsStringAsync();
            
            return !response.IsSuccessStatusCode ? $"Request failed: {body}" : AnswerFromBody(body);
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    private string AnswerFromBody(dynamic body)
    {
        dynamic jsonObject = JsonConvert.DeserializeObject(body);
        string messageContent = jsonObject.choices[0].message.content;
        return messageContent;
    }

    private string jsonBody(String question)
    {
        var body = new Completion();
        body.messages.Add(new Message(question));
        return JsonConvert.SerializeObject(body);
    }
}