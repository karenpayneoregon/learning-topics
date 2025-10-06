using BaseContextExampleApp.Classes;
using BaseContextExampleApp.Data;
using BaseContextExampleApp.Models;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace BaseContextExampleApp;
internal partial class Program
{
    static async Task Main(string[] args)
    {


        using var context = new Context();
        var birthDays = context.BirthDays.ToList();

        AnsiConsole.MarkupLine("[bold u]Id  First          Last           Birth          Age[/]");

        foreach (var item in birthDays)
        {
            Console.WriteLine($"{item.Id, -4}{item.FirstName, -15}{item.LastName, -15}{item.BirthDate, -15:MM/dd/yyyy}{item.YearsOld}");
        }


        var birthDays1 = await BirthDays();

        SpectreConsoleHelpers.ExitPrompt();
        
    }
    
    /// <summary>
    /// Retrieves a dictionary of birthdays from the database, where the key is the ID 
    /// and the value contains details such as first name, last name, and birth date.
    /// </summary>
    /// <remarks>
    /// This method asynchronously queries the database for all entries in the <see cref="Context.BirthDays"/> DbSet.
    /// It transforms the data into a dictionary where each entry's key is the ID, and the value is a <see cref="Values"/> record.
    /// </remarks>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a dictionary 
    /// with the ID as the key and a <see cref="Values"/> record as the value.
    /// </returns>
    private static async Task<Dictionary<int, Values>> BirthDays()
    {
        await using var context = new Context();

        return await context
            .BirthDays
            .Select(item => new BirthDayItem(item.Id, item.FirstName, item.LastName, item.BirthDate))
            .ToDictionaryAsync(k => k.Id, v => new Values(v.FirstName, v.LastName, v.BirthDate));
    }
}