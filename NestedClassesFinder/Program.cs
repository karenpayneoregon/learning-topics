using System.Text;
using NestedClassesFinder.Classes;
using Spectre.Console;

namespace NestedClassesFinder;
internal partial class Program
{
    static void Main(string[] args)
    {
        var path = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory.UpperFolder(5),
            "PromptFilesExamplesApp1");

        var projectFile = Directory
            .EnumerateFiles(path, "*.csproj", SearchOption.TopDirectoryOnly)
            .FirstOrDefault();

        if (projectFile is not null)
        {
            AnsiConsole.MarkupLine($"[hotpink]Scanning project:[/] [b]{Path.Combine(path, projectFile)}[/]\n");

        }

        FileNestingWriter.CreateFileNestingJson_FromMismatches(path);
        IEnumerable<(string ClassName, string FilePath)> result = NestedHelper.Find(path);
        StringBuilder sb = new();
        foreach (var (ClassName, FilePath) in result)
        {
            sb.AppendLine($"Class: {ClassName}, File: {FilePath}");
            AnsiConsole.MarkupLine($"[cyan]Class:[/] [b]{ClassName}[/]");
            AnsiConsole.MarkupLine($"  [yellow]File:[/] {FilePath.Replace(path, ".")}");
        }
        File.WriteAllText("output.txt", sb.ToString());

        SpectreConsoleHelpers.ExitPrompt();
    }
}
