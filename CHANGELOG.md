# Changelog

## V 1.0.7

### Features

- **Logs** Added more descriptive logs in console for better readability and understanding. Added Severity categories including Critical, Warning, Info, and Verbose, each with their respective console color codes.
```C#
Console.WriteLine($" [{DateTime.Now}] ({message.Severity}) -> {message.Source}: {message.Message}");
```

- **Pong!** !Ping returns Pong! :)


