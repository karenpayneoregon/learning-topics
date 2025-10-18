using System.Reflection;

namespace TempApp.Classes;
public class Special
{
    public static string GetCopyright()
    {
        var attr = Assembly
            .GetExecutingAssembly()
            .GetCustomAttribute<AssemblyCopyrightAttribute>();
        return attr?.Copyright ?? "No copyright information found.";
    }
}

/// <summary>
/// Provides utility methods to retrieve assembly metadata such as company, product, copyright, and version information.
/// </summary>
public class Info
{
    public static string GetCopyright()
    {
        var attr = Assembly
            .GetExecutingAssembly()
            .GetCustomAttribute<AssemblyCopyrightAttribute>();
        return attr?.Copyright ?? "No copyright information found.";
    }
    public static string GetCompany()
    {
        var attr = Assembly
            .GetExecutingAssembly()
            .GetCustomAttribute<AssemblyCompanyAttribute>();
        return attr?.Company ?? "No company information found.";
    }

    public static string GetProduct()
    {
        var attr = Assembly
            .GetExecutingAssembly()
            .GetCustomAttribute<AssemblyProductAttribute>();
        return attr?.Product ?? "No product information found.";
    }

    public static Version GetVersion()
    {
        return Assembly.GetExecutingAssembly().GetName().Version;
    }
}