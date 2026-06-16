using Microsoft.VisualBasic;
using Spectre.Console;
using System.Drawing;
using VPN_Detector.Classes;
using VPN_Detector.Classes.Core;
using Color = Spectre.Console.Color;

namespace VPN_Detector;
internal partial class Program
{
    static void Main(string[] args)
    {
        var (connected, vpnName) = VpnDetector.IsVpnConnected();
        Console.WriteLine(vpnName);
        AnsiConsole.MarkupLine($"[cyan]VPN Status[/] {connected.IsConnected()}");
        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

    /// <summary>
    /// Configures and displays a styled text path in the console using Spectre.Console.
    /// </summary>
    /// <remarks>
    /// This method creates a <see cref="TextPath"/> object with custom styles for the root, 
    /// separator, stem, and leaf components. The styled path is then rendered to the console.
    /// </remarks>
    private static void ConfigureAndDisplayTextPath()
    {
        var path = new TextPath("/home/user/projects/app/Program.cs")
            .RootStyle(new Style(Color.Yellow, decoration: Decoration.Bold))
            .SeparatorStyle(new Style(Color.DeepPink1))
            .StemStyle(new Style(Color.Cyan))
            .LeafStyle(new Style(Color.Green, decoration: Decoration.Underline));

        AnsiConsole.Write(path);
    }

    /// <summary>
    /// Displays a warning message in the console using Spectre.Console.
    /// </summary>
    /// <remarks>
    /// This method renders a multi-line warning message with styled text, including a yellow 
    /// "Warning" label and a dimmed message indicating that multiple issues were detected.
    /// </remarks>
    private static void DisplayWarningMessage()
    {
        var multiLine = new Markup(
            "\n\n" +
            "[yellow]Warning:[/] Multiple issues detected.\n" +
            "[dim]See log for details.[/]" +
            "\n\n"
        );
        AnsiConsole.Write(multiLine);
    }
}
