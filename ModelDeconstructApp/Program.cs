using ModelDeconstructApp.Classes;

namespace ModelDeconstructApp;
internal partial class Program
{
    static void Main(string[] args)
    {
        var list = MockedData.People();

        foreach (var (id, fullName, birthDate) in list)
        {
            Console.WriteLine($"{id, -4}{fullName,-15}{birthDate:MM/dd/yyyy}");
        }

        Console.WriteLine();

        foreach (var (_, fullName, birthDate) in list)
        {
            Console.WriteLine($"    {fullName,-15}{birthDate:MM/dd/yyyy}");
        }

        SpectreConsoleHelpers.ExitPrompt();
        
    }
}
