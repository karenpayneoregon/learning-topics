using CapitalizeFirstLetterSample.Classes.Core;
using CapitalizeFirstLetterSample.Models;
using Spectre.Console;

namespace CapitalizeFirstLetterSample
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            var people = PeopleList1();

            SpectreConsoleHelpers.ExitPrompt(Justify.Left);
        }

        private static List<Person> PeopleList1()
        {
            SpectreConsoleHelpers.PrintPink();
            List<Person> people =
            [
                new Person { Id = 1, FirstName = "John", LastName = "doe", BirthDate = new DateOnly(1990, 1, 1) },
                new Person { Id = 2, FirstName = "Jane", LastName = "Doe", BirthDate = new DateOnly(1992, 2, 2) },
                new Person { Id = 3, FirstName = "john", LastName = "Doe", BirthDate = new DateOnly(1990, 1, 1) }
            ];
            return people;
        }
    }
}
