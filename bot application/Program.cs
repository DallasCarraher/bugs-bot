using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.WebSocket;
using Discord.Commands;


namespace bot_application
{
    public class Program
    {
        private DiscordSocketClient _client;

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
            #region Connection Details (Login and Start Async)

            var _config = new DiscordSocketConfig
            {
                MessageCacheSize = 100
            };

            _client = new DiscordSocketClient(_config);
            string token = "MzE3NzUxMDcwNjQ0MjQwMzg0.DInOuQ.Jv2f8QYQXxlcrk3f736TZcGifRA";
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            #endregion

            #region Logs and Messages

            _client.Log += Logger;
            _client.MessageUpdated += MessageUpdated;
            _client.MessageReceived += MessageReceived;

            #endregion

            _client.Ready += () =>
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(" Bugs Bot successfully logged in.");
                return Task.CompletedTask;
            };

            await Task.Delay(-1);
        }


        private async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
        {
            var message = await before.GetOrDownloadAsync();
            Console.WriteLine($"{message} -> {after}");
        }


        private async Task MessageReceived(SocketMessage message)
        {
            #region Fun replies
            if (message.Content == "!ping") { await message.Channel.SendMessageAsync("Pong!"); }
            if (message.Content == "Who is your creator?" || message.Content == "Who is your Creator?") { await message.Channel.SendMessageAsync("Dallas :)"); }
            #endregion
        }


        public string GetChannelTopic(ulong id)
        {
            var channel = _client.GetChannel(352490290843484160) as SocketTextChannel;
            if (channel == null) return "";
            return channel.Topic;
        }


        public string GetGuildOwner(SocketChannel channel)
        {
            var guild = (channel as SocketGuildChannel)?.Guild;
            if (guild == null) return "";
            return guild.Owner.Username;
        }
    }
}
