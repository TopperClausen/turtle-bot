namespace Discord.Models;

public class DalleBody
{
    public string prompt { get; set; }
    public int n = 1;
    public string size { get; set; }

    public DalleBody(String prompt, string size = "1024x1024")
    {
        this.prompt = prompt;
        this.size = size;
    }
}