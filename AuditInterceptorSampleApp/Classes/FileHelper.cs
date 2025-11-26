namespace AuditInterceptorSampleApp.Classes;
public static class FileHelper
{
    /// <summary>
    /// Retrieves the full path of the most recently modified log file in the "LogFiles" directory.
    /// </summary>
    /// <returns>
    /// The full path of the newest log file, or <c>null</c> if no log files are found.
    /// </returns>
    /// <exception cref="DirectoryNotFoundException">
    /// Thrown when the "LogFiles" directory does not exist.
    /// </exception>
    public static string? GetLogFileName()
    {
        var directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogFiles");
        if (!Directory.Exists(directoryPath))
            throw new DirectoryNotFoundException($"Directory not found: {directoryPath}");

        var newestFile = Directory
            .EnumerateFiles(directoryPath, "*.txt", SearchOption.TopDirectoryOnly)
            .Select(path => new FileInfo(path))
            .OrderByDescending(f => f.LastWriteTime)
            .FirstOrDefault();

        return newestFile?.FullName;
    }
}