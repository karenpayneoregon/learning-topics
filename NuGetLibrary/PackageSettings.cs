using NuGet.Configuration;
using NuGetLibrary.Models;
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.

namespace NuGetLibrary;

/// <summary>
/// Represents the settings for managing NuGet packages, including configuration
/// and the global packages folder path.
/// </summary>
public sealed class PackageSettings
{
    private static readonly Lazy<PackageSettings> Lazy = new(() => new PackageSettings());
    public static PackageSettings Instance => Lazy.Value;

    public ISettings NuGetSettings { get; set; }

    /// <summary>
    /// Gets the path to the global NuGet packages folder.
    /// </summary>
    /// <value>
    /// A string representing the path to the global NuGet packages folder.
    /// </value>
    public string Path { get; init; }

    public List<string> DisabledSources { get; init; }

    /// <summary>
    /// Gets the list of NuGet packages
    /// </summary>
    public List<NuGetPackage> NuGetPackages { get; set; }

    private PackageSettings()
    {
        NuGetSettings = Settings.LoadDefaultSettings(null);
        Path = SettingsUtility.GetGlobalPackagesFolder(NuGetSettings);
        
        NuGetPackages = PackageWork.Packages();

        /*
         * Get the list of disabled package sources from the NuGet configuration.
         * The real purpose of this code is to show that from exploring the codebase, that there
         * is a class NuGet.Configuration.ConfigurationConstants that contains constant node names
         * to reference in the NuGet configuration file rather than hard coding them and by hard coding these
         * may cause issues in the future if the node name changes.
         */
        SettingSection? disableSources = NuGetSettings.GetSection(ConfigurationConstants.DisabledPackageSources);

        DisabledSources = disableSources!.Items.Select(x => 
            x.GetAttributes().Values.FirstOrDefault()).ToList();
    }

}