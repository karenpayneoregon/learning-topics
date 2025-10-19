using System.Reflection;

namespace ProjectPropertiesLibrary;
/// <summary>
/// Provides utility methods to retrieve assembly metadata such as company, product, copyright, and version information.
/// </summary>
public class Info
{
    private static Assembly Assembly => Assembly.GetCallingAssembly();

    /// <summary>
    /// Retrieves the copyright information from the assembly's metadata.
    /// </summary>
    /// <returns>
    /// A <see cref="string"/> containing the copyright information if available; 
    /// otherwise, "No copyright information found."
    /// </returns>
    public static string GetCopyright()
    {
        var attr = Assembly
            .GetCustomAttribute<AssemblyCopyrightAttribute>();
        return attr?.Copyright ?? "No copyright information found.";
    }
    
    /// <summary>
    /// Retrieves the company information from the assembly's metadata.
    /// </summary>
    /// <returns>
    /// A <see cref="string"/> containing the company information if available; 
    /// otherwise, "No company information found."
    /// </returns>
    public static string GetCompany()
    {
        var attr = Assembly
            .GetCustomAttribute<AssemblyCompanyAttribute>();
        return attr?.Company ?? "No company information found.";
    }

    /// <summary>
    /// Retrieves the product information from the assembly's metadata.
    /// </summary>
    /// <returns>
    /// A <see cref="string"/> containing the product information if available; 
    /// otherwise, "No product information found."
    /// </returns>
    public static string GetProduct()
    {
        var attr = Assembly
            .GetCustomAttribute<AssemblyProductAttribute>();
        return attr?.Product ?? "No product information found.";
    }

    /// <summary>
    /// Retrieves the version information from the assembly's metadata.
    /// </summary>
    /// <returns>
    /// A <see cref="Version"/> object representing the version information if available; 
    /// otherwise, a default version of 1.0.0.0.
    /// </returns>
    public static Version GetVersion()
    {
        return Assembly.GetName().Version ?? new Version(1, 0, 0, 0);
    }
}