﻿using PrintMembersSamples.Models;


namespace PrintMembersSamples.Classes;

public static class SpectreConsoleHelpers
{
    public static void ExitPrompt()
    {
        Console.WriteLine();

        Render(new Rule($"[yellow]Press[/] [cyan]ENTER[/] [yellow]to exit the demo[/]")
            .RuleStyle(Style.Parse("silver")).LeftJustified());

        Console.ReadLine();
    }

    private static void Render(Rule rule)
    {
        AnsiConsole.Write(rule);
        AnsiConsole.WriteLine();
    }

    /// <summary>
    /// Enhances the string representation of a <see cref="Person"/> object
    /// by applying color formatting to its property names using NuGet package Spectre.Console.
    /// </summary>
    /// <param name="person">The <see cref="Person"/> instance to be colorized.</param>
    /// <returns>A string representation of the <paramref name="person"/> object
    /// with colorized property names.</returns>
    public static string Colorize(this Person person) =>
        person.ToString()
            .Replace("FirstName", "[cyan]FirstName[/]")
            .Replace("LastName", "[cyan]LastName[/]")
            .Replace("Birth", "[cyan]Birth[/]")
            .Replace("SSN", "[cyan]SSN[/]")
            .Replace("PhoneNumbers", "[cyan]Phone Numbers[/]");

}