using EmojisDemoApp.Classes;
using Spectre.Console;

namespace EmojisDemoApp;
internal partial class Program
{
    static void Main(string[] args)
    {

        AnsiConsole.MarkupLine("           Gear :gear:");
        AnsiConsole.MarkupLine("         Hammer :hammer:");
        AnsiConsole.MarkupLine("    HighVoltage :high_voltage:");
        AnsiConsole.MarkupLine("    Information :information:");
        AnsiConsole.MarkupLine("        Warning :warning:");
        AnsiConsole.MarkupLine("            Cat :cat:");
        AnsiConsole.MarkupLine("      CheckMark :check_mark_button:");
        AnsiConsole.MarkupLine("      LightBulb :light_bulb:");
        AnsiConsole.MarkupLine("DiamondWithADot :diamond_with_a_dot:");
        SpectreConsoleHelpers.ExitPrompt();
    }
}
