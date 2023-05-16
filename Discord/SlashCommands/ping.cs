using Discord.WebSocket;

namespace Discord.SlashCommands
{
    public class Ping : ISlashCommand {
        public Client client { get; set; }
        public string CommandName { get { return "ping"; } set {} }


        public Ping(Client client)
        {
            this.client = client;
            var commandBuilder = SlashCommandRegister.CommandBuilder("ping", "Skriver pong tilbage");
            SlashCommandRegister.BuildCommand(commandBuilder, this.client.DiscordClient);
        }
        
        public async Task Execute(SocketSlashCommand command)
        {
            await command.RespondAsync("pong");
        }
    }
}