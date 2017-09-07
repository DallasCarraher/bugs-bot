using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace bot_application
{
//General Commands that require no sub prefix
public class General : ModuleBase<SocketCommandContext>
{
        [Command("say"), Summary("Repeats a message.")]
        [Alias("mimic", "repeat")]
        public async Task Say([Remainder, Summary("The text to repeat")] string echo)
        {
            await ReplyAsync(echo);
        }

        [Command("square"), Summary("Squares a number.")]
        public async Task Square([Summary("the number to square.")] int num)
        {
            await Context.Channel.SendMessageAsync($"{num}^2 = {Math.Pow(num, 2)}");
        }

        [Command("userinfo"), Summary("Returns information about the current user, or a user through parameter.")]
        [Alias("user", "whois")]
        public async Task UserInfo([Summary("The user to get info on.")] IUser user = null)
        {
            var userInfo = user ?? Context.Client.CurrentUser;
            await ReplyAsync($"```Username: {userInfo.Username}#{userInfo.Discriminator}\nUser Id: {userInfo.Id}\nAvatar Id: {userInfo.AvatarId}\nUser Created on: {userInfo.CreatedAt}\nUser is: {userInfo.Status}\n```");
        }

        [Command("guildinfo"), Summary("Returns information about the current guild, or a specific guild through parameter.")]
        [Alias("ginfo", "guild")]
        public async Task GuildInfo([Summary("The guild to get info on.")] ulong guildId)
        {
            try
            {
                var guild = Context.Client.GetGuild(guildId);

                EmbedBuilder emb = new EmbedBuilder();
                emb.WithAuthor(new EmbedAuthorBuilder().WithName(guild.Owner.Nickname ?? guild.Owner.Username).WithIconUrl(guild.Owner.GetAvatarUrl()));

                emb.AddField(target =>
                {
                    target.Name = "Created At";
                    target.Value = guild.CreatedAt;
                });

                emb.AddField(target =>
                {
                    target.Name = "User";
                    target.Value = guild.Users.Count;
                });

                emb.AddField(target =>
                {
                    target.Name = "Guild Name";
                    target.Value = guild.Name;
                });

                emb.WithImageUrl(guild.IconUrl);
                emb.Build();
                await ReplyAsync("",false,emb);
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
}


}