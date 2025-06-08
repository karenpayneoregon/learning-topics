using PrintMembersSamples.Models;

namespace PrintMembersSamples.Classes;

/// <summary>
/// Provides methods and functionality for managing and retrieving person-related data.
/// </summary>
public class MockedData
{
    /// <summary>
    /// Provides a sample list of people with predefined details.
    /// </summary>
    /// <returns>A list of <see cref="Person"/> objects containing sample data.</returns>
    public static List<Person> SamplePeople() =>
    [
        new("Jane", "Smith", new DateOnly(1990, 5, 22), "987-65-4321", []),
        new("Karen", "Payne", new DateOnly(1985, 1, 15), "123-45-6789", ["555-1234", "555-5678","154-3557"]),
        new("Michael", "Johnson", new DateOnly(1978, 3, 10), "987-65-4321", ["555-1111", "555-2222"]),
        new("Emily", "Davis", new DateOnly(1995, 7, 30), "456-78-9012", ["555-3333"]),
        new("David", "Wilson", new DateOnly(1982, 11, 5), "321-54-9876", ["555-4444", "555-5555"]),
    ];
}