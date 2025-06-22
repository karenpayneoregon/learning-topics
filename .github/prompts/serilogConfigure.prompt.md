Add structured logging with Serilog which writes to a folder named LogFiles under the executable folder to Main method in Program.cs. Each day there should be a folder for the current date with a file named logs.txt usng the folowwing code

```csharp

    if (builder.Environment.IsDevelopment())
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .MinimumLevel.Information()
            .WriteTo.Console()
            .CreateLogger();
    }
    else
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .MinimumLevel.Information()
            .WriteTo.File(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogFiles",
                    $"{DateTime.Now.Year}-{DateTime.Now.Month:d2}-{DateTime.Now.Day:d2}", "Log.txt"),
                rollingInterval: RollingInterval.Infinite,
                outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}")
            .CreateLogger();

    }


```
