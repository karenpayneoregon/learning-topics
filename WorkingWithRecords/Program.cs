using WorkingWithRecords.Classes;

namespace WorkingWithRecords;

internal partial class Program
{
    private static void Main(string[] args)
    {

        Console.WriteLine();
        var people = PersonRepository.SamplePeople();
        foreach (var person in people)
        {
            AnsiConsole.MarkupLine(person.Colorize());
        }
        AnsiConsole.MarkupLine("[yellow]Done[/]");
        Console.ReadLine();
    }

    private static void CreateAndDisplayPerson()
    {
        var person = new Person(
            "Karen", "Payne", 
            new DateOnly(1956, 9, 24), 
            ["123-4567", "987-6543", "555-7890"]);

        AnsiConsole.MarkupLine(person.Colorize());


        var (firstName, lastName, _, _) = person;
        AnsiConsole.MarkupLine($"[cyan]First Name:[/] {firstName} [cyan]Last Name:[/] {lastName}");
    }
}

public static class PersonExtensions 
{
    /// <summary>
    /// Enhances the string representation of a <see cref="Person"/> object
    /// by applying color formatting to its property names using NuGet package Spectre.Console.
    /// </summary>
    /// <param name="person">The <see cref="Person"/> instance to be colorized.</param>
    /// <returns>A string representation of the <paramref name="person"/> object
    /// with colorized property names.</returns>
    public static string Colorize(this Person person) => 
        person.ToString()
            .Replace("FirstName", "[cyan]FirstName[/]")
            .Replace("LastName", "[cyan]LastName[/]")
            .Replace("Birth", "[cyan]Birth[/]")
            .Replace("PhoneNumbers", "[cyan]Phone Numbers[/]");
}
