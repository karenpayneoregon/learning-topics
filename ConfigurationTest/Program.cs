#nullable enable
using System.Management;
using ConsoleConfigurationLibrary.Classes;
// ReSharper disable PossibleInvalidCastExceptionInForeachLoop


namespace ConfigurationTest;

internal partial class Program
{
    static void Main(string[] args)
    {
;
        AnsiConsole.MarkupLine("[yellow]Hello[/]");
        
        Console.WriteLine(AppConnections.Instance.MainConnection);
        Console.WriteLine(EntitySettings.Instance.CreateNew);

        Console.WriteLine();
        Console.WriteLine(ComputerInfo.GetManufacturer() ?? "Unknown");
        Console.ReadLine();
    }
}

    public static class ComputerInfo
    {
        public static string? GetManufacturer()
        {
            using var searcher = new ManagementObjectSearcher("SELECT Manufacturer FROM Win32_ComputerSystem");

            foreach (ManagementObject obj in searcher.Get())
            {
                return obj["Manufacturer"]?.ToString();
            }

            return null;
        }
    }