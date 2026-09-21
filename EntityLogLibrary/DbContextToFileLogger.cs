namespace EntityLogLibrary;
/// <summary>
/// For logging messages from DbContext.
/// 
/// DO NOT use in production, only development environment
/// 
/// Totally void of exception handling as this is for use by a developer on their machine
/// were they need the proper permissions to create and write to files.
/// 
/// </summary>
/// <summary>
/// Provides a mechanism for logging Entity Framework Core (EF Core) database context activities to a file.
/// </summary>
/// <remarks>
/// This class is intended for development purposes only and should not be used in production environments.
/// It supports both asynchronous and synchronous logging of messages to a specified file.
///
/// Also, an NuGet package which is out of date.
/// https://www.nuget.org/packages/EntityCoreFileLogger/
///
/// This code is current
/// 
/// </remarks>
public class DbContextToFileLogger
{
    private readonly string _fileName;
    private static readonly SemaphoreSlim _gate = new(1, 1);

    public DbContextToFileLogger(string fileName)
    {
        _fileName = fileName;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DbContextToFileLogger"/> class with a default log file path.
    /// </summary>
    /// <remarks>
    /// The default log file path is determined based on the application's base directory and includes a folder named "LogFiles" 
    /// with the current date (in the format "YYYY-MM-DD") and a file named "EF_Log.txt".
    /// This constructor is primarily intended for local development purposes.
    /// </remarks>
    public DbContextToFileLogger()
    {
        var baseDir = AppDomain.CurrentDomain.BaseDirectory; // OK for local dev
        var folder = Path.Combine(baseDir, "LogFiles",
            $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}");
        _fileName = Path.Combine(folder, "EF_Log.txt");
    }

    public async Task LogAsync(string message, CancellationToken ct = default)
    {
        
        // Make sure the directory exists
        Directory.CreateDirectory(Path.GetDirectoryName(_fileName)!);

        await _gate.WaitAsync(ct).ConfigureAwait(false);
        
        try
        {
            // Single open, append mode, allow others to read/write if they also opt in
            await using var fs = new FileStream(
                _fileName,
                FileMode.Append,
                FileAccess.Write,
                FileShare.ReadWrite,     // <-- important
                bufferSize: 4096,
                FileOptions.Asynchronous | FileOptions.WriteThrough);

            await using var writer = new StreamWriter(fs);
            await writer.WriteLineAsync(message.AsMemory(), ct);
            await writer.WriteLineAsync(new string('-', 40).AsMemory(), ct);
            await writer.FlushAsync(ct);
        }
        finally
        {
            _gate.Release();
        }
    }

    // Optional sync shim if you must keep the old signature
    public void Log(string message) => LogAsync(message).GetAwaiter().GetResult();
}
