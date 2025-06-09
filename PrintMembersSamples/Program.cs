using PrintMembersSamples.Classes;
using PrintMembersSamples.Models;

namespace PrintMembersSamples;

internal partial class Program
{
    static void Main(string[] args)
    {
        DisplayPeopleWithDetails();
        //DisplayAddressExample();
        SpectreConsoleHelpers.ExitPrompt();
    }

    private static void DisplayPeopleWithDetails()
    {
        var people = MockedData.SamplePeople();
        AnsiConsole.MarkupLine("[cyan]First     Last      SSN          Birthdate     UserName  Password    Phone numbers[/]");
        foreach (var person in people)
        {
            AnsiConsole.MarkupLine(person.Colorize());
        }
    }

    private static void DisplayAddressExample()
    {
        var addresses = new List<Address>
    {
        new("123 Main St", null, "Portland", "USA", "97201"),
        new("456 Maple Ave", "Apt 4B", "Eugene", "USA", "97401"),
        new("789 Oak Dr", "", "Salem", "USA", "97301")
    };

        foreach (var address in addresses)
        {
            Console.WriteLine(address);
        }
    }
}