using Discord.Net;
using Discord.WebSocket;

namespace Discord.SlashCommands
{
    public static class SlashCommandRegister {
        public static SlashCommandBuilder CommandBuilder(string commandName, string description)
        {
            var guildCommand = new SlashCommandBuilder();
            guildCommand.WithName(commandName);
            guildCommand.WithDescription(description);
            return guildCommand;
        }

        public static async Task BuildCommand(SlashCommandBuilder guildCommand, DiscordSocketClient client)
        {
            var guild = client.GetGuild(Convert.ToUInt64(Environment.GetEnvironmentVariable("GUILD_ID")));
            try
            {
                await guild.CreateApplicationCommandAsync(guildCommand.Build());
            }
            catch(ApplicationCommandException exception)
            {
                Console.WriteLine(exception.Reason);
            }
        }
    }    
}
