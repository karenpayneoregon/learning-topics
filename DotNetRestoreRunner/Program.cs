using DotNetRestoreRunner.Classes.Core;
using Spectre.Console;
using System.Diagnostics;

namespace DotNetRestoreRunner;

internal partial class Program
{
    static async Task<int> Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage:");
            Console.WriteLine(@"  DotNetRestoreRunner.exe C:\Projects\MySolution");
            return 1;
        }

        var workingDirectory = args[0];

        if (!Directory.Exists(workingDirectory))
        {
            AnsiConsole.MarkupLine($"[red bold]Directory does not exist:[/] {workingDirectory}");
            return 1;
        }

        AnsiConsole.MarkupLine($"[CornflowerBlue]Working directory:[/] {workingDirectory}");
        AnsiConsole.MarkupLine("[CornflowerBlue]Running dotnet restore...[/]");
        Console.WriteLine();

        var startInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = "restore",
            WorkingDirectory = workingDirectory,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        using var process = new Process
        {
            StartInfo = startInfo
        };

        process.OutputDataReceived += (_, e) =>
        {
            if (e.Data is not null)
            {
                Console.WriteLine(e.Data);
            }
        };

        process.ErrorDataReceived += (_, e) =>
        {
            if (e.Data is not null)
            {
                Console.Error.WriteLine(e.Data);
            }
        };

        try
        {
            process.Start();

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            await process.WaitForExitAsync();

            Console.WriteLine();

            if (process.ExitCode == 0)
            {
                AnsiConsole.MarkupLine("[cyan]dotnet restore completed successfully.[/]");
                SpectreConsoleHelpers.ExitPrompt(Justify.Left);  
            }
            else
            {
                AnsiConsole.MarkupLine($"[red bold]dotnet restore failed with exit code {process.ExitCode}.[/]");

            }

            return process.ExitCode;
        }
        catch (Exception exception)
        {
            AnsiConsole.MarkupLine($"[red bold]Unable to run dotnet restore: {exception.Message}[/]");
            return 1;
        }
    }
}