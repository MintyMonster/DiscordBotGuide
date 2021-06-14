using Discord;
using Discord.Net;
using Discord.WebSocket;
using Discord.Commands;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TestingDiscordBot
{
    public class Commands : ModuleBase
    {
        [Command("hello")]
        public async Task HelloCommand()
        {
            var sb = new StringBuilder();

            string user = Context.User.Username;

            sb.AppendLine($"Hello there, {user}!");

            await ReplyAsync(sb.ToString());
        }

        [Command("specialhello")]
        public async Task specialHelloCommand()
        {
            var sb = new StringBuilder();
            var embed = new EmbedBuilder();
            string user = Context.User.Username;
            var rnd = new Random();
            var r = rnd.Next(0, 256);
            var g = rnd.Next(0, 256);
            var b = rnd.Next(0, 256);

            sb.AppendLine($"Hello there, {user}!");

            embed.Title = "Hello!";
            embed.Description = sb.ToString();
            embed.Color = new Color(r, g, b);

            await ReplyAsync(null, false, embed.Build());
        }
    }
}
