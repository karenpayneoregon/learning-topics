using ConsoleConfigurationLibrary.Classes;

namespace ConfigurationTest;

internal partial class Program
{
    static void Main(string[] args)
    {
;
        AnsiConsole.MarkupLine("[yellow]Hello[/]");
        
        Console.WriteLine(AppConnections.Instance.MainConnection);
        Console.WriteLine(EntitySettings.Instance.CreateNew);
        Console.ReadLine();
    }
}