using Discord;

namespace TurtleBot
{
    public class Program
    {
        public static Task Main(string[] args) => new Program().MainAsync();

        public async Task MainAsync()
        {
            DotEnv.SetEnv();
            await new Client().Run();       
        }
    }
}