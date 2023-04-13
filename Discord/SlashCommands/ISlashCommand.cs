using Discord.WebSocket;

namespace Discord.SlashCommands
{
    public interface ISlashCommand {
        public string CommandName { get; set; }
        Task Execute(SocketSlashCommand command);
    }
}