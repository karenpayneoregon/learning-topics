using IterateSamplesApp.Classes;
using static IterateSamplesApp.Classes.GenericIterators;

namespace IterateSamplesApp;
internal partial class Program
{
    static void Main(string[] args)
    {
        Iterate(1, 2, 3, 4, 5);
        Console.WriteLine();
        
        Iterate("Sam", "Anne", "Mary", "Dan", "Kim");
        Console.WriteLine();
        
        Mocked.GetCategories().Iterate(c => $"{c.Id,-3} {c.Name}");
        Console.WriteLine();
        
        Mocked.GetCategories().Iterate(category => category.ToString());
        
        SpectreConsoleHelpers.ExitPrompt();
    }
}
