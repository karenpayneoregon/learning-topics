using ModelDeconstructApp.Classes;

namespace ModelDeconstructApp;
internal partial class Program
{
    static void Main(string[] args)
    {
        var list = MockedData.People();

        foreach (var person in list)
        {
            var (id, fullName, birthDate) = person;
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
