using System.Text;
using NestedClassesFinder.Classes;
using NestedClassesFinder.Classes.Standard;

namespace NestedClassesFinder;
internal partial class Program
{
    static void Main(string[] args)
    {

        var path = "C:\\OED\\DotnetLand\\VS2022\\CodeExperiments\\GeneratedRegexApp";

        FileNestingWriter.CreateFileNestingJson(path);
        IEnumerable<(string ClassName, string FilePath)> result = NestedHelper.Find(path);
        StringBuilder sb = new();
        foreach (var (ClassName, FilePath) in result)
        {
            sb.AppendLine($"Class: {ClassName}, File: {FilePath}");
            Console.WriteLine($"Class: {ClassName}");
            Console.WriteLine($"  File: {FilePath}");
        }
        File.WriteAllText("output.txt", sb.ToString());
        Console.WriteLine("Done");
        SpectreConsoleHelpers.ExitPrompt();
    }
}
