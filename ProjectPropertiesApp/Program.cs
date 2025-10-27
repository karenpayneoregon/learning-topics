using ProjectPropertiesApp.Classes;
using ProjectPropertiesLibrary;

namespace ProjectPropertiesApp;
internal partial class Program
{
    static void Main(string[] args)
    {
        var copyright = Info.GetCopyright();
        Console.WriteLine(copyright);
        Console.WriteLine(Info.GetCompany());
        Console.WriteLine();
        var company = Info.GetCompany(out var who);
        Console.WriteLine($"Company: {company}");
        Console.WriteLine($"Called by: {who.TypeName}.{who.MethodName}");
        Console.WriteLine($"Assembly: {who.AssemblyName} v{who.AssemblyVersion}");
        Console.WriteLine($"Source  : {who.FilePath}:{who.LineNumber}");
        SpectreConsoleHelpers.ExitPrompt();
    }
}
