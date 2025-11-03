using CommonHelpersLibrary;
using Isopoh.Cryptography.Argon2;
using PasswordGeneratorArgonSample.Classes;
using Spectre.Console;


namespace PasswordGeneratorArgonSample;
internal partial class Program
{
    static void Main(string[] args)
    {
        var password = "password1";
        var hash = GetHash(password);
        var isValid = Argon2.Verify(hash, password);
        AnsiConsole.MarkupLine($"[cyan]Valid?[/] {isValid.ToYesNo()}");
        SpectreConsoleHelpers.ExitPrompt();
    }

    static string GetHash(string password) => Argon2.Hash(password);
}
