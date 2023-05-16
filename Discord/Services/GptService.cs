using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace Discord.Services;
using Discord.Models.GPT;
using System.Net.Http;
public class GptService
{
    private String endpoint = "https://api.openai.com/v1/chat/completions";
    public async Task<String> Ask(String question)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", Environment.GetEnvironmentVariable("OPENAI_TOKEN"));
        var content = new StringContent(jsonBody(question), Encoding.UTF8, "application/json");

        try
        {
            HttpResponseMessage response = await client.PostAsync(endpoint, content);

            var body =  await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                dynamic jsonObject = JsonConvert.DeserializeObject(body);
                string messageContent = jsonObject.choices[0].message.content;
                return messageContent;
            }
            else
            {
                return $"Request failed: {body}";
            }
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    private string jsonBody(String question)
    {
        var body = new Completion();
        body.messages.Add(new Message(question));
        return JsonConvert.SerializeObject(body);
    }
}