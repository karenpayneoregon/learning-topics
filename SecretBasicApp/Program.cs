using SecretBasicApp.Classes.Core;
using Spectre.Console;

namespace SecretBasicApp;
internal partial class Program
{
    static void Main(string[] args)
    {
        var connectionString = SecretReader.Instance.ConnectionString;

        AnsiConsole.MarkupLine($"[bold yellow]Connection String:[/] {connectionString}");
        
        Console.WriteLine();
        
        var mailSettings = SecretReader.Instance.MailSettings;
        AnsiConsole.MarkupLine($"[bold yellow]Mail Settings From:[/] {mailSettings.FromAddress}");
        AnsiConsole.MarkupLine($"[bold yellow]Mail Settings Port:[/] {mailSettings.Port}");
        AnsiConsole.MarkupLine($"[bold yellow]Mail Settings Pickup:[/] {mailSettings.PickupFolder}");
        
        Console.WriteLine();
        
        var login = SecretReader.Instance.Login;
        AnsiConsole.MarkupLine($"[bold yellow]Login UserName:[/] {login.UserName}");
        AnsiConsole.MarkupLine($"[bold yellow]Login Password:[/] {login.Password}");


        var path = UniqueTextFileNameGenerator.Create(directory: mailSettings.PickupFolder, prefix: "export");

        Console.WriteLine();
        AnsiConsole.MarkupLine($"[bold yellow]Mail drop file:[/] {path}");

        
        SpectreConsoleHelpers.ExitPrompt();
    }
}
