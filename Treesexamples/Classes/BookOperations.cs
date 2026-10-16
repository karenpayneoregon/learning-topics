using Spectre.Console;
using System.Diagnostics;
using System.Text.Json;
using Treesexamples.Models;
using TreesExamples.Classes.Core;
using TreesExamples.Models;

namespace TreesExamples.Classes;

internal class BookOperations
{
    /// <summary>
    /// Groups a collection of books by predefined price ranges and displays the results in a tree structure.
    /// </summary>
    /// <remarks>
    /// This method performs the following steps:
    /// <list type="number">
    /// <item>Deserializes a JSON string into a list of <see cref="Book"/> objects.</item>
    /// <item>Groups the books into price ranges using the <see cref="GroupBooksByPriceRange"/> method.</item>
    /// <item>Displays the grouped books in a tree structure using Spectre.Console.</item>
    /// </list>
    /// The tree structure categorizes books by price range and includes details such as ID, title, and price.
    /// </remarks>

    public static void BooksGroupings()
    {

        SpectreConsoleHelpers.PrintPink();
        
        List<Book> books = JsonSerializer.Deserialize<List<Book>>(Json())!;

        IEnumerable<IGrouping<string, Book>> grouped = GroupBooksByPriceRange(books);

        var tree = new Tree("[bold yellow]Books by Price Range[/]");

        foreach (var (price, booksGroup) in grouped.Select(group
                     => (group.Key, group)))
        {
            var priceNode = tree.AddNode($"[bold cyan]{Markup.Escape(price)}[/]");

            foreach (var book in booksGroup)
            {
                priceNode.AddNode(
                    $"[green]Id:[/] {book.Id}, " +
                    $"[green]Title:[/] {Markup.Escape(book.Title)}, " +
                    $"[green]Price:[/] {book.Price:C}");
            }
        }

        AnsiConsole.Write(tree);
    }


    /// <summary>
    /// Provides a JSON string representation of a predefined collection of books.
    /// </summary>
    /// <remarks>
    /// The JSON string contains an array of book objects, each with properties such as
    /// <c>Id</c>, <c>Title</c>, and <c>Price</c>. This method is used to supply sample
    /// data for operations involving books.
    /// </remarks>
    /// <returns>
    /// A JSON string representing a collection of books.
    /// </returns>
    private static string Json()
    {
        var json =
            /* lang=json*/
            """
            [
              {
                "Id": 1,
                "Title": "Learn EF Core",
                "Price": 19.0
              },
              {
                "Id": 2,
                "Title": "C# Basics",
                "Price": 18.0
              },
              {
                "Id": 3,
                "Title": "ASP.NET Core advance",
                "Price": 30.0
              },
              {
                "Id": 4,
                "Title": "VB.NET To C#",
                "Price": 9.0
              },
              {
                "Id": 5,
                "Title": "Basic Azure",
                "Price": 59.0
              }
            ]
            """;

        return json;

    }

    /// <summary>
    /// Groups a collection of books into price ranges.
    /// </summary>
    /// <param name="books">
    /// A list of <see cref="Book"/> objects to be grouped by price range.
    /// </param>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> of <see cref="IGrouping{TKey, TElement}"/> where the key is a string
    /// representing the price range and the elements are the books within that range.
    /// </returns>
    private static IEnumerable<IGrouping<string, Book>> GroupBooksByPriceRange(List<Book> books)
    => books.GroupBy(b => b switch
    {
        { Price: < 10 } => "Under $10",
        { Price: >= 10 and < 20 } => "$10 to $19",
        { Price: >= 20 and < 30 } => "$20 to $29",
        { Price: >= 30 } => "$30 and above",
        _ => "Unknown"
    });
}
