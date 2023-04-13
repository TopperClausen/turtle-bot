using Discord.WebSocket;

namespace Discord.SlashCommands
{
    public class Ping : ISlashCommand {
        public DiscordSocketClient client { get; set; }
        
        public string CommandName { get { return "ping"; } set {} }
        
        public Ping(DiscordSocketClient client)
        {
            this.client = client;
            SlashCommandRegister.Register(client, CommandName, "Skriver pong tilbage");
        }
        
        public async Task Execute(SocketSlashCommand command)
        {
            await command.RespondAsync("pong");
        }
    }
}