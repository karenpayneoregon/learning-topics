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

        var projectFile = ProjectFile();

        if (projectFile is not null)
        {
            AnsiConsole.MarkupLine($"[hotpink]Scanning project:[/] [b]{Path.Combine(path, projectFile)}[/]\n");

        }

        FileNestingWriter.CreateFileNestingJson_FromMismatches(path);
        
        IEnumerable<(string ClassName, string FilePath)> result = NestedHelper.Find(path);

        StringBuilder sb = new();
        foreach (var (className, filePath) in result)
        {
            sb.AppendLine($"Class: {className}, File: {filePath}");
            AnsiConsole.MarkupLine($"[cyan]Class:[/] [b]{className}[/]");
            AnsiConsole.MarkupLine($"  [yellow]File:[/] {filePath.Replace(path, ".")}");
        }
        File.WriteAllText("output.txt", sb.ToString());

        SpectreConsoleHelpers.ExitPrompt();

        string? ProjectFile() =>
            Directory
                .EnumerateFiles(path, "*.csproj", SearchOption.TopDirectoryOnly)
                .FirstOrDefault();
    }
}
