using Discord.Net;
using Discord.SlashCommands;
using Discord.WebSocket;
using Discord.Services;

namespace Discord {
    public class Client {
        public DiscordSocketClient DiscordClient { get; set; }
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
            Console.WriteLine("command " + command.CommandName + " issued by " + command.User.Username);
            
            if (Environment.GetEnvironmentVariable("ENVIROMENT") == "DEVELOPMENT" && command.Channel.Id == 496370645962063898)
            {
                SlashCommands.Find(slashCommand => slashCommand.CommandName == command.CommandName).Execute(command);
            }
            else if (Environment.GetEnvironmentVariable("ENVIROMENT") == "PRODUCTION" && command.Channel.Id != 496370645962063898)
            {
                SlashCommands.Find(slashCommand => slashCommand.CommandName == command.CommandName).Execute(command);
            }

        }
        
        private async Task ClientReady()
        {
            RegisterCommands();
            
            DiscordSocketClient client = new DiscordSocketClient();
        }
        
        private void RegisterCommands()
        {
            Console.WriteLine("registering commands");
            SlashCommands.Add(new Ping(this));
            SlashCommands.Add(new Gpt(this));
            SlashCommands.Add(new Dalle(this));
        }
        
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}