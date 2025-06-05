using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace PrintMembersSamples;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        AnsiConsole.MarkupLine("");
        Console.Title = "PrintMembers sample";
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
    }

    private static Table CreateTable()
    {
        return new Table()
            .RoundedBorder().BorderColor(Color.LightSlateGrey)
            .AddColumn("[b]First[/]")
            .AddColumn("[b]Last[/]")
            .AddColumn("[b]SSN[/]")
            .AddColumn("[b]BirthDate[/]")
            .Alignment(Justify.Center)
            .Title("[yellow]Data[/]");
    }


}
