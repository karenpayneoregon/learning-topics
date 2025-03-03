using ConsoleConfigurationLibrary.Models;
using CustomIConfigurationSourceSample.Classes;
using CustomIConfigurationSourceSample.Data;
using CustomIConfigurationSourceSample.Models;
using Microsoft.Extensions.Configuration;

namespace CustomIConfigurationSourceSample;

internal partial class Program
{
    static void Main(string[] args)
    {
  
        var builder = new ConfigurationBuilder();
        builder.Add(new DatabaseConfigurationSource()); 
        var configuration = builder.Build();

        var connectionStrings = configuration.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>();
        string mainConnection = connectionStrings.MainConnection;
        AnsiConsole.MarkupLine($"[cyan]Connection String:[/] {mainConnection}");

        string phone = configuration.GetValue<string>(nameof(HelpDesk.Phone));
        AnsiConsole.MarkupLine($"[cyan]Phone:[/] {phone}");

        string email = configuration.GetValue<string>(nameof(HelpDesk.Email));
        AnsiConsole.MarkupLine($"[cyan]Email:[/] {email}");


        Console.ReadLine();
    }
}