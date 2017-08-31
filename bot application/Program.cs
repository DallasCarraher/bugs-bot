using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace bot_application
{
    public class Program
    {
        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;

        public async Task MainAsync()
        {
            #region Connection Details
            _client = new DiscordSocketClient();
            _client.Log += Log;
            string token = "MzE3NzUxMDcwNjQ0MjQwMzg0.DInOuQ.Jv2f8QYQXxlcrk3f736TZcGifRA";
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            #endregion

            #region Msg Reception
            _client.MessageReceived += MessageReceived;
            #endregion

            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private async Task MessageReceived(SocketMessage message)
        {
            #region Fun replies
            if (message.Content == "!ping") { await message.Channel.SendMessageAsync("Pong!"); }
            if (message.Content == "What is Bailey?" || message.Content == "What is bailey?") { await message.Channel.SendMessageAsync("A smelly adidas loving pleb :)"); }
            if (message.Content == "What is Dallas?" || message.Content == "What is Dallas?") { await message.Channel.SendMessageAsync("An intelligent and handsome fun-loving guy ;)"); }
            if (message.Content == "What is Ethan?" || message.Content == "What is Ethan?") { await message.Channel.SendMessageAsync("A good guy greg :)"); }
            #endregion


        }

        private async Task DirectMessageReceived(SocketDMChannel Directmessage)
        {

        }
    }
}
