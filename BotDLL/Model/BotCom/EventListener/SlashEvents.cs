using System;
using System.Threading.Tasks;

using DisCatSharp;
using DisCatSharp.EventArgs;
using DisCatSharp.ApplicationCommands;
using DisCatSharp.ApplicationCommands.EventArgs;

using Microsoft.Extensions.Logging;

namespace BotDLL.EventListener
{
    /// <summary>
    /// The slash events.
    /// </summary>
    class SlashEvents
    {
        public static Task Discord_ApplicationCommandUpdated(DiscordClient sender, ApplicationCommandEventArgs e)
        {
            sender.Logger.LogInformation($"Shard {sender.ShardId} sent application command updated: {e.Command.Name}: {e.Command.Id} for {e.Command.ApplicationId}");
            return Task.CompletedTask;
        }
        public static Task Discord_ApplicationCommandDeleted(DiscordClient sender, ApplicationCommandEventArgs e)
        {
            sender.Logger.LogInformation($"Shard {sender.ShardId} sent application command deleted: {e.Command.Name}: {e.Command.Id} for {e.Command.ApplicationId}");
            return Task.CompletedTask;
        }
        public static Task Discord_ApplicationCommandCreated(DiscordClient sender, ApplicationCommandEventArgs e)
        {
            sender.Logger.LogInformation($"Shard {sender.ShardId} sent application command created: {e.Command.Name}: {e.Command.Id} for {e.Command.ApplicationId}");
            return Task.CompletedTask;
        }
        public static Task Ac_SlashCommandExecuted(ApplicationCommandsExtension sender, SlashCommandExecutedEventArgs e)
        {
            Console.WriteLine($"Slash/Info: {e.Context.CommandName}");
            return Task.CompletedTask;
        }

        public static Task Ac_SlashCommandErrored(ApplicationCommandsExtension sender, SlashCommandErrorEventArgs e)
        {
            Console.WriteLine($"Slash/Error: {e.Exception.Message} | CN: {e.Context.CommandName} | IID: {e.Context.InteractionId}");
            return Task.CompletedTask;
        }

        public static Task Ac_ContextMenuExecuted(ApplicationCommandsExtension sender, ContextMenuExecutedEventArgs e)
        {
            Console.WriteLine($"Slash/Info: {e.Context.CommandName}");
            return Task.CompletedTask;
        }

        public static Task Ac_ContextMenuErrored(ApplicationCommandsExtension sender, ContextMenuErrorEventArgs e)
        {
            Console.WriteLine($"Slash/Error: {e.Exception.Message} | CN: {e.Context.CommandName} | IID: {e.Context.InteractionId}");
            return Task.CompletedTask;
        }
    }
}
