using Spectre.Console;
using WineConsoleApp.Classes;
using WineConsoleApp.Data;
using WineConsoleApp.Models;
using static WineConsoleApp.Classes.AnsiConsoleHelpers;

namespace WineConsoleApp.Classes;

/// <summary>
/// Provides methods for demonstrating various looping constructs in C#
/// to iterate through and display collections of wines.
/// </summary>
/// <remarks>
/// This class includes examples of traditional for loops, foreach loops, 
/// and indexed foreach loops, with specific handling for wines of type <see cref="WineType.Red"/>.
/// </remarks>
internal class LearningForAndWhile
{
    private static Color _redColor = Color.DeepPink3;

    /// <summary>
    /// Iterates through a collection of wines and displays each wine.
    /// </summary>
    /// <remarks>
    /// This method uses a traditional for loop to process a collection of wines.
    /// Wines of type <see cref="WineType.Red"/> are displayed with a specific color.
    /// </remarks>
    public static void TraditionalForStatement()
    {

        PrintCyan();

        var wines = AppData.Instance.Wines;

        for (int index = 0; index < wines.Count; index++)
        {
            if (wines[index].WineType == WineType.Red)
            {
                AnsiConsole.MarkupLine($"[{_redColor}]{wines[index]}[/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"{wines[index]}");
            }
        }

        Console.WriteLine();

    }

    /// <summary>
    /// Iterates through a collection of wines and displays each wine.
    /// </summary>
    /// <remarks>
    /// This method uses a traditional foreach loop to process a collection of wines.
    /// Wines of type <see cref="WineType.Red"/> are displayed with a specific color.
    /// </remarks>
    public static void TraditionalForEachStatement()
    {

        PrintCyan();

        var wines = AppData.Instance.Wines;

        foreach (var wine in wines)
        {
            if (wine.WineType == WineType.Red)
            {
                AnsiConsole.MarkupLine($"[{_redColor}]{wine}[/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"{wine}");
            }
        }

        Console.WriteLine();

    }

    /// <summary>
    /// Iterates through a collection of wines with their respective indices and displays each wine.
    /// </summary>
    /// <remarks>
    /// This method uses an indexed foreach loop to process a collection of wines.
    /// Wines of type <see cref="WineType.Red"/> are displayed with a specific color.
    /// </remarks>
    public static void ForEachWithIndexStatement()
    {

        PrintCyan();

        var wines = AppData.Instance.Wines;

        foreach (var (index, wine) in wines.Index())
        {
            if (wine.WineType == WineType.Red)
            {
                AnsiConsole.MarkupLine($"[{_redColor}]{index,-5}{wine}[/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"{index,-5}{wine}");
            }
        }

        Console.WriteLine();

    }

    public static void WhileLoop()
    {
        PrintCyan();

        var wines = AppData.Instance.Wines;
        int index = 0;

        while (index < wines.Count)
        {
            if (wines[index].WineType == WineType.Red)
            {
                AnsiConsole.MarkupLine($"[{_redColor}]{index, -5}{wines[index]}[/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"{index, -5}{wines[index]}");
            }
            index++;
        }

        Console.WriteLine();
    }

}
