using System;
using Discord;
using Discord.Net;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace TestingDiscordBot
{
    class Program
    {
        private DiscordSocketClient _client;
        
        static void Main(String[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public Program()
        {
            _client = new DiscordSocketClient();

            _client.Log += LogAsync;

            _client.Ready += ReadyAsync;

        }

        public async Task MainAsync()
        {

            using(var services = ConfigureServices())
            {
                var client = services.GetRequiredService<DiscordSocketClient>();
                _client = client;

                _client.Log += LogAsync;
                _client.Ready += ReadyAsync;
                services.GetRequiredService<CommandService>().Log += LogAsync;

                await _client.LoginAsync(TokenType.Bot, "Insert Token Here");
                await _client.StartAsync();

                await services.GetRequiredService<CommandHandler>().InitialiseAsync();

                await Task.Delay(-1);
            }
        }

        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }

        private Task ReadyAsync()
        {
            Console.WriteLine($"Connected as -> TesterBot!");
            return Task.CompletedTask;
        }

        private ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandler>()
                .BuildServiceProvider();
        }
    }
}
