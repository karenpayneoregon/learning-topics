using FindUserSecretsApp.Classes;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace FindUserSecretsApp;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        AnsiConsole.MarkupLine("");
        Console.Title = "Get user secrets";
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
        SetupLogging.Development();
    }
    public static JsonSerializerOptions Indented => new() { WriteIndented = true };


    [GeneratedRegex(@"<UserSecretsId>(.*?)</UserSecretsId>", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex GenerateUserSecretsIdRegex();
}
