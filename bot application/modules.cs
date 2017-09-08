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
        [Command("help"), Summary("Lists to the user available commands")]
        [Alias("?")]
        public async Task Help(IUser user = null)
        {
            var userDM = user ?? Context.User;

            EmbedBuilder emb = new EmbedBuilder();
            emb.WithAuthor(new EmbedAuthorBuilder().WithName(userDM.Username).WithIconUrl(userDM.GetAvatarUrl()));

            emb.AddField(target =>
            {
                target.Name = "Eh, What's up Doc?";
                target.Value = "Here's a list of all Commands";
            });

            emb.AddField(target =>
            {
                target.Name = "​";
                target.Value = "​";
            });

            emb.AddField(target =>
            {
                target.Name = "Command Prefix  ' . '";
                target.Value = "​";
            });

            emb.AddField(target =>
            {
                target.Name = "Help  or  ?";
                target.Value = "Lists all available user commands.";
            });

            emb.AddField(target =>
            {
                target.Name = "Say [msg]";
                target.Value = "Repeats a message.";
            });

            emb.AddField(target =>
            {
                target.Name = "User [userid]";
                target.Value = "Returns information about the current user, or a user through parameter.";
            });

            emb.AddField(target =>
            {
                target.Name = "Guild [guildid]";
                target.Value = "Returns information about the current guild, or a specific guild through parameter.";
            });

            emb.AddField(target =>
            {
                target.Name = "Square [int]";
                target.Value = "Squares a number.";
            });

            //emb.WithImageUrl("https://pbs.twimg.com/media/CybWy_WW8AAYyZ4.jpg");
            emb.Build();
            await userDM.SendMessageAsync("", false, emb);

            //await userDM.SendMessageAsync($"What's up Doc? \t\t`{userDM.Username}#{userDM.Discriminator}`\n\n**Here is a list of available Commands** \n` .commandName [ parameter(s) ] `\n\n\n`help || ?`  will send you the list of commands\n\n`say || mimic || repeat`  will repeat a message you type as a parameter\n\n`square`  will square a number :)\n\n`userinfo || user || whois`  will return information about the current user, or a specific user if given a user ID as a parameter\n\n`guildinfo || ginfo || guild`  will return information about a specific guild through Id as parameter\n\n", false);
        }

        [Command("say"), Summary("Repeats a message.")]
        [Alias("mimic", "repeat")]
        public async Task Say([Remainder, Summary("The text to repeat")] string echo)
        {
            try
            {
                await ReplyAsync(echo);
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        [Command("square"), Summary("Squares a number.")]
        public async Task Square([Summary("the number to square.")] int num)
        {
            try
            {
                await Context.Channel.SendMessageAsync($"{num}^2 = {Math.Pow(num, 2)}");
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        [Command("userinfo"), Summary("Returns information about the current user, or a user through parameter.")]
        [Alias("user", "whois")]
        public async Task UserInfo([Summary("The user to get info on.")] IUser user = null)
        {
            try
            {
                var userInfo = user ?? Context.Client.CurrentUser;
                await ReplyAsync($"```Username: {userInfo.Username}#{userInfo.Discriminator}\nUser Id: {userInfo.Id}\nAvatar Id: {userInfo.AvatarId}\nUser Created on: {userInfo.CreatedAt}\nUser is: {userInfo.Status}\n```");
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
            }
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
                    target.Name = "Created On";
                    target.Value = guild.CreatedAt;
                });

                emb.AddField(target =>
                {
                    target.Name = "User Count";
                    target.Value = guild.Users.Count;
                });

                emb.AddField(target =>
                {
                    target.Name = "Guild Name";
                    target.Value = guild.Name;
                });

                emb.AddField(target =>
                {
                    target.Name = "Verification lvl";
                    target.Value = guild.VerificationLevel;
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