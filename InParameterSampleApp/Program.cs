using InParameterSampleApp.Classes.Behind;
using InParameterSampleApp.Models;
using Spectre.Console;

namespace InParameterSampleApp;
internal partial class Program
{
    static void Main(string[] args)
    {
        var book = new Book()
        {
            Price = 44.99m, Title = "Learn C#", CategoryId = 1, Category = new Category() { CategoryId = 1, Description = "Programming" }
        };

        BookDetails(in book);
        BookDetails(book);

        SpectreConsoleHelpers.ExitPrompt();
    }

    private static void BookDetails(in Book book)
    {
        AnsiConsole.MarkupLine($"[cyan]{book.Title,-12}{book.Price,-8:C}{book.Category.Description}[/]");
        //book = new Book(); // This line will cause a compile-time error
    }

    private static void BookDetails(Book book)
    {
        AnsiConsole.MarkupLine($"[yellow]{book.Title,-12}{book.Price,-8:C}{book.Category.Description}[/]");

        book = new Book()
        {
            Price = 11.99m, Title = "Learn TypeScript", CategoryId = 1,  Category = new Category() { CategoryId = 1, Description = "Programming" }
        };
    }
}
