using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;

namespace AuditInterceptorSampleApp.Classes;
public static class FileHelper
{
 
    /// <summary>
    /// Retrieves the most recently created log file from the "LogFiles" directory.
    /// </summary>
    /// <returns>
    /// A <see cref="FileInfo"/> object representing the newest log file, or <c>null</c> if no log files are found.
    /// </returns>
    /// <exception cref="DirectoryNotFoundException">
    /// Thrown when the "LogFiles" directory does not exist.
    /// </exception>
    public static FileInfo? GetLogFileName()
    {
        var rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogFiles");
        var pattern = "**/*.txt";


        if (!Directory.Exists(rootPath))
            throw new DirectoryNotFoundException(rootPath);

        var matcher = new Matcher();
        matcher.AddInclude(pattern);

        var directoryInfo = new DirectoryInfo(rootPath);
        var dirWrapper = new DirectoryInfoWrapper(directoryInfo);

        var matchResult = matcher.Execute(dirWrapper);

        var newest = matchResult.Files
            .Select(f => new FileInfo(Path.Combine(rootPath, f.Path)))
            .OrderByDescending(f => f.LastWriteTimeUtc)
            .FirstOrDefault();
        
        return newest;
    }
}