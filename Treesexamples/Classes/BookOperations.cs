using System.Diagnostics;
using System.Text.Json;
using Treesexamples.Models;
using TreesExamples.Models;

namespace TreesExamples.Classes;

internal class BookOperations
{
    /// <summary>
    /// Groups a predefined collection of books by price ranges and outputs the grouped results.
    /// </summary>
    /// <remarks>
    /// This method deserializes a JSON string containing book data into a list of <see cref="Book"/> objects.
    /// It then groups the books into price ranges using the <see cref="GroupBooksByPriceRange"/> method.
    /// The grouped results are written to the debug output for inspection.
    /// </remarks>
    public static void BooksGroupings()
    {

        List<Book> books = JsonSerializer.Deserialize<List<Book>>(Json())!;

        IEnumerable<IGrouping<string, Book>> results = GroupBooksByPriceRange(books);

        foreach (var (price, booksGroup) in results.Select(group
                     => (group.Key, group)))
        {

            Console.WriteLine($"Price Range: {price}");

            foreach (var book in booksGroup)
            {
                Console.WriteLine($"\tId: {book.Id}, Title: {book.Title}, Price: {book.Price:C}");
            }

        }
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
