using ConsoleConfigurationLibrary.Classes;
using PromptFilesExamplesApp.Classes;
using PromptFilesExamplesApp.Models;
using static PromptFilesExamplesApp.Classes.SpectreConsoleHelpers;

namespace PromptFilesExamplesApp;

internal partial class Program
{
    static void Main(string[] args)
    {

        GroupCustomersByGender();
        Console.ReadLine();
    }

    private static void GroupCustomersByGender()
    {
        var genderGroups = BogusCustomer.GenerateCustomers(20)
            .GroupBy(c => c.Gender)
            .Select(g => new 
            {
                Gender = g.Key,
                Count = g.Count(),
                List = g.ToList()
            }).ToList();

        foreach (var cg in genderGroups)
        {
            AnsiConsole.MarkupLine($"[cyan]{cg.Gender}[/]: [b]{cg.Count}[/]");
            foreach (var customer in cg.List)
            {
                Console.WriteLine($"   {customer.Id,-5}{customer.FirstName,-12} {customer.LastName,-15} {customer.Email}");
            }
        }
    }

}

