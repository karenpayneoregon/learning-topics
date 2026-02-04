using IComparerIEqualityComparerApp.Classes.Comparers;
using IComparerIEqualityComparerApp.Models;
using Spectre.Console;
using IComparerIEqualityComparerApp.Classes.SystemCode;

namespace IComparerIEqualityComparerApp.Classes;
internal class Operations
{
    /// <summary>
    /// Demonstrates the use of the <see cref="FirstNameLastNameBirthDateComparer"/> 
    /// to filter a list of <see cref="Person"/> objects and display only distinct entries 
    /// based on their first name, last name, and birthdate.
    /// </summary>
    /// <remarks>
    /// This method creates a list of <see cref="Person"/> objects, applies the 
    /// <see cref="FirstNameLastNameBirthDateComparer"/> to identify distinct entries, 
    /// and then prints the distinct people to the console.
    /// </remarks>
    public static void DistinctPeople1()
    {
        SpectreConsoleHelpers.PrintCyan();
        
        var people = new List<Person>
        {
            new Person { Id = 1, FirstName = "John", LastName = "Doe", BirthDate = new DateOnly(1990, 1, 1) },
            new Person { Id = 2, FirstName = "Jane", LastName = "Doe", BirthDate = new DateOnly(1992, 2, 2) },
            new Person { Id = 3, FirstName = "john", LastName = "Doe", BirthDate = new DateOnly(1990, 1, 1) },
        };

        var distinctPeople = people.Distinct(new FirstNameLastNameBirthDateComparer()).ToList();

        AnsiConsole.MarkupLine("[bold]Distinct People:[/]");
        foreach (var person in distinctPeople)
        {
            AnsiConsole.MarkupLine($"[DeepPink3]{person.FirstName,8} {person.LastName} {person.BirthDate}[/]");
        }

        Console.WriteLine();
    }

    /// <summary>
    /// Filters a list of <see cref="Person"/> objects to remove duplicates based on their 
    /// <see cref="Person.FirstName"/>, <see cref="Person.LastName"/>, and <see cref="Person.BirthDate"/> properties.
    /// </summary>
    /// <remarks>
    /// This method demonstrates the use of the <c>DistinctBy</c> LINQ method to identify unique 
    /// <see cref="Person"/> objects in a collection.
    /// </remarks>
    public static void DistinctPeople2()
    {

        SpectreConsoleHelpers.PrintCyan();
        
        var people = new List<Person>
        {
            new Person { Id = 1, FirstName = "John", LastName = "doe", BirthDate = new DateOnly(1990, 1, 1) },
            new Person { Id = 2, FirstName = "Jane", LastName = "Doe", BirthDate = new DateOnly(1992, 2, 2) },
            new Person { Id = 3, FirstName = "john", LastName = "Doe", BirthDate = new DateOnly(1990, 1, 1) },
        };

        var count = people.Count;

        var distinctPeople = people.DistinctBy(x =>
        (
            x.FirstName,
            x.LastName,
            x.BirthDate
        )).ToList();

        var distinctCount = distinctPeople.Count;

        foreach (var person in distinctPeople)
        {
            AnsiConsole.MarkupLine($"[DeepPink3]{person.FirstName,8} {person.LastName} {person.BirthDate}[/]");
        }
    }

    /// <summary>
    /// Creates and returns a list of <see cref="Person"/> objects, sorted by their last names.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="Person"/> objects, ordered by the <see cref="Person.LastName"/> property.
    /// </returns>
    public static List<Person> PeopleDataList()
    {

        SpectreConsoleHelpers.PrintCyan();
        
        List<Person> peopleList =
        [
            new() { Id = 1, FirstName = "Mike", LastName = "Williams", BirthDate = new DateOnly(1956,9,24)},
            new() { Id = 1, FirstName = "Karen", LastName = "Payne", BirthDate = new DateOnly(1956, 9, 24) },
            new() { Id = 2, FirstName = "Sam", LastName = "Smith", BirthDate = new DateOnly(1976, 3, 4) },
            new() { Id = 1, FirstName = "Karen", LastName = "Payne", BirthDate = new DateOnly(1956, 9, 24) }
        ];

        return peopleList.OrderBy(x => x.LastName).ToList();
    }


    /// <summary>
    /// Compares two lists of <see cref="Product"/> objects to determine if their details have changed.
    /// </summary>
    /// <remarks>
    /// This method uses the <see cref="ProductComparer"/> to compare two lists of <see cref="Product"/> objects.
    /// It checks if the products in the updated list are equal to those in the original list based on their 
    /// name and description. If any differences are found, a message indicating the changes is displayed.
    ///
    /// Article Reference:
    /// https://tomjones.dev/blog/handling-list-comparisons-in-net-with-iequalitycomparer/
    /// - code samples have issues
    /// - Karen changed the code samples to work correctly and conformed to <see cref="FirstNameLastNameBirthDateComparer"/>
    /// </remarks>
    public static void CompareProducts()
    {

        SpectreConsoleHelpers.PrintCyan();
        
        var originalProducts = new List<Product>()
        {
            new() { Id = 1, Name = "ProductName1", Description = "ProductDescription1", Price = 199.99f },
            new() { Id = 2, Name = "ProductName2", Description = "ProductDescription2", Price = 29.99f },
        };

        var updatedProducts = new List<Product>()
        {
            new() { Id = 1, Name = "ProductName1", Description = "UpdatedProductDescription1", Price = 199.99f },
            new() { Id = 2, Name = "UpdatedProductName2", Description = "ProductDescription2", Price = 29.99f },
        };

        AnsiConsole.MarkupLine("[bold]Products SequenceEqual[/]");

        var productDetailsHaveChanged = !updatedProducts.SequenceEqual(originalProducts, new ProductComparer());

        AnsiConsole.MarkupLine(productDetailsHaveChanged
            ? "    [bold DeepPink3]Product details have changed.[/]"
            : "    [bold DeepPink3]Product details have not changed.[/]");

        Console.WriteLine();
    }
}
