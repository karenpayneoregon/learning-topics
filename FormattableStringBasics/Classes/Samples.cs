using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FormattableStringBasics.Models;
using Spectre.Console;

namespace FormattableStringBasics.Classes;
internal class Samples
{
    public static void People()
    {
        List<Person> list = SimulateFromDatabase();

        IEnumerable<FormattableString> items = list.Select(p =>
            FormattableStringFactory.Create(
                "[lightslateblue]{0,-4}{1,-8}{2,-8}{3}[/]", 
                p.Id, p.FirstName, p.LastName, p.BirthDate));

        AnsiConsole.MarkupLine("[yellow]No formatted output[/]");
        foreach (var fs in items)
        {
            Console.WriteLine($"    {fs.Id(),-4}{fs.FirstName(),-8}{fs.LastName(),-8}{fs.BirthDate()}");
        }

        Console.WriteLine();

        AnsiConsole.MarkupLine("[yellow]Formatted output[/]");
        foreach (var fs in items)
        {
            AnsiConsole.MarkupLine($"    {fs}");
        }

        Console.WriteLine();

        AnsiConsole.MarkupLine("[yellow]Find by last name, get first name[/]");
        Console.WriteLine($"    {items.FirstOrDefault(x => x.LastName() == "Smith").FirstName()}");
    }
    private static List<Person> SimulateFromDatabase()
        =>
        [
            new() { Id = 1, FirstName = "Karen", LastName = "Payne", BirthDate = new(1962, 12, 7) },
            new() { Id = 2, FirstName = "Sam", LastName = "Smith", BirthDate = new(1972, 8, 15) },
            new() { Id = 3, FirstName = "Lucy", LastName = "Adams", BirthDate = new(1982, 2, 25) }
        ];

}
