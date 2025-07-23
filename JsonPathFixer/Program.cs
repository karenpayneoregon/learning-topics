using JsonPathFixer.Classes;

namespace JsonPathFixer;

internal partial class Program
{
    static void Main(string[] args)
    {
        var path = Helpers.GetPath();
        if (!string.IsNullOrWhiteSpace(path))
        {
            Console.WriteLine(Helpers.FormatPathForJson(path));
        }
        Console.ReadLine();
    }
}