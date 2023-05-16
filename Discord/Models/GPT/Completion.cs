namespace Discord.Models.GPT;

public class Completion
{
    public String model = "gpt-3.5-turbo";
    public List<Message> messages = new List<Message>();
}