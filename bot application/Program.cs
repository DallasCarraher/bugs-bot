using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace bot_application
{
    public class Program
    {
        private DiscordSocketClient client;
        private CommandService commands;
        private IServiceProvider service;

        //Program entrypoint
        //Calls constructor, then Async method, then waits for it to finish (it shouldn't)
        public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        //Logging handler
        private Task Logger(LogMessage message)
        {
            var cc = Console.ForegroundColor;
            switch (message.Severity)
            {
                case LogSeverity.Critical:
                case LogSeverity.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogSeverity.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogSeverity.Info:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogSeverity.Verbose:
                case LogSeverity.Debug:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
            }

            Console.WriteLine($" [{DateTime.Now}] ({message.Severity}) -> {message.Source}: {message.Message}");
            Console.ForegroundColor = cc;
            return Task.CompletedTask;
        }

        //Main Async Task
        public async Task MainAsync()
        {
            #region Startup

            var _config = new DiscordSocketConfig
            {
                MessageCacheSize = 100
            };

            client = new DiscordSocketClient(_config);
            commands = new CommandService();

            string token = "MzE3NzUxMDcwNjQ0MjQwMzg0.DmUadw.FFvyYWBUQ5nZr7o_gPie4JDAkU4";

            service = new ServiceCollection().BuildServiceProvider();

            await InstallCommands();

            await client.LoginAsync(TokenType.Bot, token, true);
            await client.StartAsync();

            #endregion

            #region Logs and Messages

            client.Log += Logger;
            client.MessageUpdated += MessageUpdated;
            client.MessageReceived += MessageReceived;

            #endregion

            client.Ready += () =>
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(" Bugs Bot successfully logged in.");
                return Task.CompletedTask;
            };

            await Task.Delay(-1);
        }

        public async Task InstallCommands()
        {
            //Hook MessageReceived Event into Command Handler
            client.MessageReceived += HandleCommand;
            await commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        public async Task HandleCommand(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;
            if (message == null) return; // Don't process the command if it was a System message
            int argPos = 0; //num to track where prefix ends and the command begins

            if (!(message.HasCharPrefix('.', ref argPos) || message.HasMentionPrefix(client.CurrentUser, ref argPos))) return;

            var context = new SocketCommandContext(client, message);

            var result = await commands.ExecuteAsync(context, argPos, service);

            if (!result.IsSuccess)
            {
                await context.Channel.SendMessageAsync(result.ErrorReason);
            }
        }

        private async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
        {
            var message = await before.GetOrDownloadAsync();
            Console.WriteLine($"User Updated Text from {message} -> {after}");
        }
        
        private async Task MessageReceived(SocketMessage message)
        {
            #region Fun replies
            if (message.Content == "!ping") { await message.Channel.SendMessageAsync("Pong!"); }
            if (message.Content == "Who is your creator?" || message.Content == "Who is your Creator?") { await message.Channel.SendMessageAsync("Dallas :)"); }
            #endregion
        }
        
    }
}
