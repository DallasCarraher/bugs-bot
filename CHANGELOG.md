# Changelog

## V 1.0.8

### Features

- **Commands** Added some starter commands to the bot to kick off front-end functionality. Most importantly I've implemented a 'help' command that will send the user a direct message of all existing commands at their disposal with a format fitting for it's importance. I was able to accomplish this with an embed using the EmbedBuilder object in the framework API.

the code looks like this:

```C#
EmbedBuilder emb = new EmbedBuilder();
emb.WithAuthor(new EmbedAuthorBuilder().WithName(userDM.Username).WithIconUrl(userDM.GetAvatarUrl()));

emb.AddField(target =>
{
target.Name = "Eh, What's up Doc?";
target.Value = "List of Available Commands\n\n";
});
```

- Some other commands that are noteworthy are the 'userinfo' and 'guildinfo' commands. They both return information in a similar format using embeds for users and guilds.


## V 1.0.7

### Features

- **Logs** Added more descriptive logs in console for better readability and understanding. Added Severity categories including Critical, Warning, Info, and Verbose, each with their respective console color codes.
```C#
Console.WriteLine($" [{DateTime.Now}] ({message.Severity}) -> {message.Source}: {message.Message}");
```

- **Pong!** !Ping returns Pong! :)


