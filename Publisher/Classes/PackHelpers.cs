using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Publisher.Classes;

/// <summary>
/// Provides helper methods for managing NuGet packages, including publishing and copying packages
/// to a local feed, as well as opening the local feed folder.
/// </summary>
/// <remarks>
/// This class relies on the configuration provided by <see cref="ApplicationSettings"/> for the
/// NuGet executable path and the local packages location.
/// </remarks>
public class PackHelpers
{
    
    /// <summary>
    /// Gets the path to the NuGet CLI executable.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> representing the full path to the NuGet CLI executable.
    /// </value>
    /// <remarks>
    /// This property retrieves the value from the <see cref="ApplicationSettings.NuGetExecutable"/> property.
    /// Ensure that the path is correctly configured in the application settings.
    /// </remarks>
    public static string NuGetExecutable => ApplicationSettings.Instance.NuGetExecutable;
    
    /// <summary>
    /// Gets the file system path where local NuGet packages are stored.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> representing the local packages location configured in the application settings.
    /// </value>
    /// <remarks>
    /// This property retrieves the value of <see cref="ApplicationSettings.LocalPackagesLocation"/>.
    /// Ensure that the local packages location is correctly configured in the application settings.
    /// </remarks>
    public static string PackageLocation => ApplicationSettings.Instance.LocalPackagesLocation;

    /// <summary>
    /// Publishes a NuGet package to the local feed.
    /// </summary>
    /// <param name="package">The full path of the package to be published.</param>
    /// <param name="showCommand">
    /// If <see langword="true"/>, the command used for publishing the package is copied to the Windows Clipboard.
    /// </param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <remarks>
    /// This method uses the NuGet CLI to add the specified package to the local feed.
    /// Ensure that the <see cref="ApplicationSettings.NuGetExecutable"/> and 
    /// <see cref="ApplicationSettings.LocalPackagesLocation"/> are correctly configured.
    /// </remarks>
    [Obsolete("This method is outdated. Use CopyPackageAsync() instead.", true)]
    public static async Task Publish(string package, bool showCommand = false)
    {
        if (showCommand) Clipboard.SetText($"{NuGetExecutable} add {package} -source {PackageLocation}");

        var start = new ProcessStartInfo
        {
            FileName = NuGetExecutable,
            RedirectStandardOutput = true,
            Arguments = $"add {package} -source {PackageLocation}",
            CreateNoWindow = true
        };


        using var process = Process.Start(start);
        await process.WaitForExitAsync();

        File.Copy(package, Path.Combine(PackageLocation, Path.GetFileName(package)), true);
    }

    
    /// <summary>
    /// Copies a NuGet package to the local feed asynchronously.
    /// </summary>
    /// <param name="source">The full path of the source package to be copied.</param>
    /// <param name="showCommand">
    /// If <see langword="true"/>, the command used for copying the package is copied to the Windows Clipboard.
    /// </param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <remarks>
    /// This method ensures that the destination directory exists before copying the package.
    /// The destination is determined by <see cref="ApplicationSettings.LocalPackagesLocation"/>.
    /// </remarks>
    public static async Task CopyPackageAsync(string source, bool showCommand = false)
    {
        if (showCommand) Clipboard.SetText($"{NuGetExecutable} add {source} -source {PackageLocation}");

        var destDir = PackHelpers.PackageLocation;
        var destPath = Path.Combine(destDir, Path.GetFileName(source));

        // Ensure destination directory exists
        Directory.CreateDirectory(destDir);

        await using FileStream sourceStream = new FileStream(source, FileMode.Open, FileAccess.Read, FileShare.Read);
        await using FileStream destStream = new FileStream(destPath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 81920, useAsync: true);
        await sourceStream.CopyToAsync(destStream);
    }
    
    
    /// <summary>
    /// Opens the local feed folder in the default file explorer.
    /// </summary>
    /// <remarks>
    /// The folder path is determined by <see cref="ApplicationSettings.LocalPackagesLocation"/>.
    /// Ensure that the path is correctly configured before invoking this method.
    /// </remarks>
    public static void OpenFeedFolder()
    {
        var start = new ProcessStartInfo
        {
            FileName = PackageLocation,
            UseShellExecute = true
        };

        Process.Start(start);
    }
}