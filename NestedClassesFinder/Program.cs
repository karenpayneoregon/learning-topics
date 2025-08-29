using System.Text;
using NestedClassesFinder.Classes;

namespace NestedClassesFinder;
internal partial class Program
{
    static void Main(string[] args)
    {
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.UpperFolder(5), "PromptFilesExamplesApp1");
        Console.WriteLine($"Scanning: {path}");
        FileNestingWriter.CreateFileNestingJson_FromMismatches(path);
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
