using System.Reflection;
using System.Runtime.Versioning;

namespace AssemblyApp.Classes;

public static class FrameworkExtensions
{
    /// <summary>
    /// Retrieves the target framework version of the specified assembly.
    /// </summary>
    /// <param name="assembly">
    /// The <see cref="System.Reflection.Assembly"/> instance from which to retrieve the target framework version.
    /// </param>
    /// <returns>
    /// A <see cref="System.Version"/> representing the target framework version if available; otherwise, <c>null</c>.
    /// </returns>
    /// <remarks>
    /// This method attempts to extract the framework version from the <see cref="System.Runtime.Versioning.TargetFrameworkAttribute"/> 
    /// applied to the assembly. If the framework version cannot be determined, it returns <c>null</c>.
    /// </remarks>
    public static Version? GetTargetFrameworkVersion(this Assembly assembly)
    {
        
        var framework = assembly.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName;

        if (string.IsNullOrWhiteSpace(framework))
            return null;

        const string marker = "Version=v";
        var id = framework.IndexOf(marker, StringComparison.OrdinalIgnoreCase);
        if (id >= 0)
        {
            var versionText = framework[(id + marker.Length)..].Trim();
            var comma = versionText.IndexOf(',');
            if (comma >= 0) versionText = versionText[..comma];

            return Version.TryParse(versionText, out var v) ? v : null;
        }

        var lastSpace = framework.LastIndexOf(' ');
        if (lastSpace < 0 || lastSpace + 1 >= framework.Length) return Version.TryParse(framework, out var direct) ? direct : null;
        {
            var versionText = framework[(lastSpace + 1)..].Trim();
            return Version.TryParse(versionText, out var v) ? v : null;
        }

    }
    
    /// <summary>
    /// Retrieves the target framework version of the entry assembly.
    /// </summary>
    /// <returns>
    /// A <see cref="System.Version"/> representing the target framework version of the
    /// entry assembly if available; otherwise, <c>null</c>.
    /// </returns>
    /// <remarks>
    /// This method internally calls <see cref="FrameworkExtensions.GetTargetFrameworkVersion(System.Reflection.Assembly)"/> 
    /// on the entry assembly obtained via <see cref="System.Reflection.Assembly.GetEntryAssembly"/>.
    /// If the entry assembly is <c>null</c> or its framework version cannot be determined,
    /// this method returns <c>null</c>.
    /// </remarks>
    public static Version? GetTargetFrameworkVersion() 
        => Assembly.GetEntryAssembly()?.GetTargetFrameworkVersion();
}