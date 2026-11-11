using ConsoleNet11CodeApp1.Classes.Core;
using Spectre.Console;

namespace ConsoleNet11CodeApp1;

internal partial class Program
{
    static void Main(string[] args)
    {
        //DisplayWithIndex();
        UnionHandleProductLookup();

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

    private static void UnionHandleProductLookup()
    {
        
        SpectreConsoleHelpers.PrintPink();
        
        ProductLookup result = new Product(42, "Mechanical keyboard");

        var message = result switch
        {
            Product product => $"Found {product.Name}",
            ProductNotFound missing => $"No product with ID {missing.Id}",
        };

        AnsiConsole.MarkupLine($"[green bold]{message}[/]");
    }

    /// <summary>
    /// Displays a list of tags along with their corresponding indices in the console.
    /// </summary>
    /// <remarks>
    /// The method uses a <see cref="HashSet{T}"/> to store tags with case-insensitive comparison
    /// and iterates through the tags using their indices. The output is formatted with colors
    /// using the Spectre.Console library.
    /// </remarks>
    private static void DisplayWithIndex()
    {

        SpectreConsoleHelpers.PrintPink();
        
        HashSet<string> names = [with(StringComparer.OrdinalIgnoreCase), "Alice", "ALICE", "Bob"];
        HashSet<string> tags = [with(StringComparer.OrdinalIgnoreCase), "one", "two", "three"];

        foreach (var (index, tag) in tags.Index())
        {
            AnsiConsole.MarkupLine($"[green]{index}[/]: [cyan]{tag}[/]");
        }
        
    }
}


public sealed record Product(int Id, string Name);
public sealed record ProductNotFound(int Id);

public union ProductLookup(Product, ProductNotFound);