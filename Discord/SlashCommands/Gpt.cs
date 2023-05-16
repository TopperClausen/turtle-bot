using Discord.Services;
using Discord.WebSocket;

namespace Discord.SlashCommands;

public class Gpt : ISlashCommand
{
    public Client client { get; set; }
    public string CommandName { get { return "gpt"; } set {} }
    private GptService _gptService = new GptService();

    public Gpt(Client client)
    {
        this.client = client;
        var commandBuilder = SlashCommandRegister.CommandBuilder("gpt", "Stil Chat GPT et spørgsmål");
        commandBuilder.AddOption("question", ApplicationCommandOptionType.String, "Spørgsmål", true);
        SlashCommandRegister.BuildCommand(commandBuilder, this.client.DiscordClient);
    }
    public async Task Execute(SocketSlashCommand command)
    {
        var param = command.Data.Options.First(param => param.Name == "question");
        string question = command.Data.Options.First(param => param.Name == "question").Value.ToString();
        command.RespondAsync(question);
        
        string response = await _gptService.Ask(param.Value.ToString());
        await command.FollowupAsync(response);
    }
}