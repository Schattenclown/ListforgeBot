using BotDLL.Model.BotCom.DiscordCommands.Base;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotDLL.Model.BotCom.DiscordCommands
{
    public class HelpCommand : IDiscordCommandAsync
    {
        private Embed _message;

        public HelpCommand(string servers, string statisticsservers)
        {
            _message = GenerateMessage(servers, statisticsservers);
        }

        private Embed GenerateMessage(string servers, string statisticsservers)
        {
            EmbedBuilder embedBuilder = new EmbedBuilder
            {
                Description = $"```!help shows help\n" +
                        $"\n" +
                        $"!42big show every server\n" +
                        $"!42 show every server with less info\n" +
                        $"!42s show playerstatistics for every server\n" +
                        $"\n" +
                        $"!list show serverlist\n" +
                        $"\n" +
                        $"Serverinfo from single server\n" +
                        $"{servers}\n" +
                        $"\n" +
                        $"Playerstatistics from single server\n" +
                        $"{statisticsservers}\n" +
                        $"(✿◠‿◠) ty for using me```",
                Color = Color.LightOrange,
                Title = "cmd info"
            };

            return embedBuilder.Build();
        }

        public Task<bool> Execute()
        {
            return Task.FromResult(true);
        }

        public Task<Embed> GetMessage()
        {
            return Task.FromResult(_message);
        }
    }
}
