using Discord.Net;
using Discord.WebSocket;

namespace Discord.SlashCommands
{
    public static class SlashCommandRegister {
        public static async Task Register(DiscordSocketClient client, string commandName, string description)
        {
            var guild = client.GetGuild(Convert.ToUInt64(Environment.GetEnvironmentVariable("GUILD_ID")));
            var guildCommand = new SlashCommandBuilder();
            guildCommand.WithName(commandName);
            guildCommand.WithDescription(description);
            
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
