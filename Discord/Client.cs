using Discord.Net;
using Discord.SlashCommands;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace Discord {
    public class Client {
        private DiscordSocketClient DiscordClient { get; set; }
        private List<ISlashCommand> SlashCommands = new List<ISlashCommand>();
        
        public async Task Run()
        {
            DiscordClient = new DiscordSocketClient();

            DiscordClient.Log += Log;
            var token = Environment.GetEnvironmentVariable("TOKEN");
            await DiscordClient.LoginAsync(TokenType.Bot, token);
            await DiscordClient.StartAsync();
            DiscordClient.Ready += ClientReady;
            DiscordClient.SlashCommandExecuted += SlashCommandHandler;
            
            await Task.Delay(-1);
        }
        
        private async Task SlashCommandHandler(SocketSlashCommand command)
        {
            SlashCommands.Find(command => command.CommandName == command.CommandName).Execute(command);
        }
        
        private async Task ClientReady()
        {
            RegisterCommands();
        }
        
        private void RegisterCommands()
        {
            Console.WriteLine("registering commands");
            SlashCommands.Add(new Ping(DiscordClient));
        }
        
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}