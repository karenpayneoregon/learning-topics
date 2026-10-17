
#nullable disable
namespace TreesExamples.Models;
/// <summary>
/// Represents a book with properties such as ID, title, and price.
/// </summary>
/// <remarks>
/// This class is part of the <c>TreesExamples.Models</c> namespace and is used to model book data.
/// It includes properties for identifying the book, its title, and its price, which may be nullable.
/// </remarks>
public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal? Price { get; set; }
}
