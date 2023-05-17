using Discord.Services;
using Discord.WebSocket;

namespace Discord.SlashCommands;

public class Dalle : ISlashCommand
{
    public Client client { get; set; }
    private DalleService _dalleService = new DalleService();
    public string CommandName { get { return "dalle"; } set {} }
    public async Task Execute(SocketSlashCommand command)
    {
        string question = command.Data.Options.First(param => param.Name == "question").Value.ToString();
        command.RespondAsync(question);
        string url = await _dalleService.Ask(question);

        command.FollowupAsync(url);
    }

    public Dalle(Client client)
    {
        this.client = client;
        var commandBuilder = SlashCommandRegister.CommandBuilder(CommandName, "Få et genereret billede fra DALL-E");
        commandBuilder.AddOption("question", ApplicationCommandOptionType.String, "prompt", true);
        SlashCommandRegister.BuildCommand(commandBuilder, this.client.DiscordClient);
    }
}