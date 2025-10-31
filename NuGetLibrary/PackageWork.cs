using System.Text;
using System.Text.RegularExpressions;
using NuGet.Common;
using NuGet.Configuration;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;
using NuGetLibrary.Models;

namespace NuGetLibrary;

public partial class PackageWork
{
    /// <summary>
    /// Retrieves a list of available NuGet packages from the global packages folder.
    /// </summary>
    /// <returns>A list of <see cref="Package"/> objects representing the available packages.</returns>
    public static List<Package> AvailablePackages()
    {
        List<Package> packages = [];

        var nugetPath = PackageSettings.Instance.Path;

        foreach (string packageFolder in Directory.GetDirectories(nugetPath))
        {
            string packageName = Path.GetFileName(packageFolder);

            packages.AddRange(Directory.GetDirectories(packageFolder)
                .Select(Path.GetFileName)
                .Where(version => VersionRegex().IsMatch(version!))
                .Select(version => new Package { Name = packageName, Version = version }));
        }

        return packages;

    }
    /// <summary>
    /// Displays the available NuGet packages grouped by their names.
    /// </summary>
    /// <remarks>
    /// This method retrieves all available packages, groups them by their names, 
    /// and writes the grouped information, including package versions, to a file named "GroupedPackages.txt".
    /// </remarks>
    public static void DisplayPackagesGroupedByName()
    {
        var packages = AvailablePackages();

        IOrderedEnumerable<IGrouping<string, Package>> groupedPackages = packages
            .GroupBy(p => p.Name)
            .OrderBy(g => g.Key); 

        StringBuilder sb = new();
        
        foreach (var group in groupedPackages)
        {
            sb.AppendLine($"Package: {group.Key}");
            foreach (var pkg in group.OrderBy(p => p.Version))
            {
                sb.AppendLine($"  {pkg.Version}");
            }
            sb.AppendLine(); // spacing
        }

        File.WriteAllText("GroupedPackages.txt", sb.ToString());
    }


    /// <summary>
    /// Retrieves a list of NuGet package sources, including their names, sources, and enabled statuses.
    /// </summary>
    /// <returns>A list of <see cref="NuGetPackage"/> objects representing the package sources.</returns>
    public static List<NuGetPackage> Packages()
    {
        List<NuGetPackage> list = [];

        ISettings settings = Settings.LoadDefaultSettings(null);

        PackageSourceProvider packageSourceProvider = new PackageSourceProvider(settings);
        var packageSources = packageSourceProvider.LoadPackageSources();

        foreach (var source in packageSources)
        {
            
            list.Add(new NuGetPackage()
            {
                Name = source.Name, 
                Source = source.Source,
                Enabled = source.IsEnabled,
                HasCredentials = source.Credentials != null
            });

        }

        return list;
    }

    /// <summary>
    /// Filters the provided list of NuGet packages to return only those that match the specified package name.
    /// </summary>
    /// <param name="packages">The list of <see cref="Package"/> objects to search through.</param>
    /// <param name="packageName">The name of the package to filter by.</param>
    /// <returns>A list of <see cref="Package"/> objects whose names match the specified <paramref name="packageName"/>.</returns>
    public static List<Package> GetPackagesByName(List<Package> packages, string packageName) 
        => packages.Where(x => x.Name == packageName).ToList();

    [GeneratedRegex(@"^(?=(.*\.){2,})(?=(.*\d){2,}).*$")]
    private static partial Regex VersionRegex();
}

