using FieldKeywordSample.Classes;
using FieldKeywordSample.Interfaces;
using FieldKeywordSample.Models;

namespace FieldKeywordSample;

internal partial class Program
{
    static void Main(string[] args)
    {
        var people = MockedData.People();

        foreach (var person in people)
        {
            {

                if (person is Customer customer)
                {
                    AnsiConsole.MarkupLine($"[cyan]{customer.Id} {customer.FirstName} {customer.LastName} {customer.BirthDate}[/]");
                }
                else
                {
                    AnsiConsole.MarkupLine($"[green]{person.Id} {person.FirstName} {person.LastName} {person.BirthDate}[/]");
                }
            }

            Console.WriteLine();

            {
                if (person is not Customer customer) continue;

                AnsiConsole.MarkupLine("[cyan]Customer[/]");
                Console.WriteLine($"    Customer ID: {customer.CustomerId}");
                Console.WriteLine();

                if (customer.Addresses is null) continue;

                AnsiConsole.MarkupLine("[cyan]    Address[/]");
                foreach (var address in customer.Addresses)
                {
                    Console.WriteLine($"        {address.FullAddress}");
                }

            }

            Console.WriteLine();
        }

        AnsiConsole.MarkupLine("[yellow]Exit[/]");
        Console.ReadLine();
    }


}