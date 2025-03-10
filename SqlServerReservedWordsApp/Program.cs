namespace SqlServerReservedWordsApp;

internal partial class Program
{
    static void Main(string[] args)
    {

        string input = "select";
        if (Utilities.SqlServerReservedWords().Contains(input))
        {
            Console.WriteLine($"'{input}' is a SQL Server reserved keyword.");
        }
        else
        {
            Console.WriteLine($"'{input}' is NOT a SQL Server reserved keyword.");
        }

        Console.WriteLine();
        AnsiConsole.MarkupLine("[yellow]Done[/]");
        Console.ReadLine();
    }
}