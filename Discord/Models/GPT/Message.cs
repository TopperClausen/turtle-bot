namespace Discord.Models.GPT;

public class Message
{
    public String role = "user";
    public String content { get; set; }

    public Message(String message)
    {
        this.content = message;
    }
}