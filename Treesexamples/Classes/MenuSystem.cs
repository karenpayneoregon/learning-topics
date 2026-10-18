using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;
using TreesExamples.Models;

namespace TreesExamples.Classes;

public static class MenuSystem
{
    private static readonly List<MenuItem> Menu =
    [
        new()
        {
            Title = "File Operations",
            Children =
            [
                new()
                {
                    Title = "Create File",
                    Action = () => ShowMessage("Creating file...")
                },
                new()
                {
                    Title = "Open File",
                    Action = () => ShowMessage("Opening file...")
                },
                new()
                {
                    Title = "Delete File",
                    Action = () => ShowMessage("Deleting file...")
                }
            ]
        },
        new()
        {
            Title = "Reports",
            Children =
            [
                new()
                {
                    Title = "Sales Report",
                    Action = () => ShowMessage("Sales report")
                },
                new()
                {
                    Title = "Customer Report",
                    Action = () => ShowMessage("Customer report")
                }
            ]
        },
        new()
        {
            Title = "Settings",
            Children =
            [
                new()
                {
                    Title = "Appearance",
                    Action = () => ShowMessage("Appearance settings")
                },
                new()
                {
                    Title = "Preferences",
                    Action = () => ShowMessage("Preferences")
                }
            ]
        }
    ];

    public static void Run()
    {
        ShowMenu(Menu, "Application Menu");
    }

    private static void ShowMenu(List<MenuItem> items, string title, bool isRoot = true)
    {
        while (true)
        {
            AnsiConsole.Clear();

            AnsiConsole.Write(
                new Rule($"[yellow]{Markup.Escape(title)}[/]")
                    .LeftJustified());

            DisplayTree(items, title);

            AnsiConsole.WriteLine();

            var choices = items.Select(item => item.Title).ToList();
            choices.Add(isRoot ? "Exit" : "Back");

            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]Select an option[/]")
                    .PageSize(10)
                    .AddChoices(choices));

            if (selection == (isRoot ? "Exit" : "Back"))
                return;

            var selectedItem = items.First(
                item => item.Title == selection);

            if (selectedItem.Children.Count > 0)
            {
                ShowMenu(selectedItem.Children, selectedItem.Title, false);
            }
            else
            {
                selectedItem.Action?.Invoke();
            }
        }
    }

    private static void DisplayTree(List<MenuItem> items, string title)
    {
        var tree = new Tree($"[bold yellow]{Markup.Escape(title)}[/]");

        foreach (var item in items)
        {
            AddNode(tree, item);
        }

        AnsiConsole.Write(tree);
    }

    private static void AddNode(IHasTreeNodes parent, MenuItem item)
    {
        var node = parent.AddNode($"[cyan]{Markup.Escape(item.Title)}[/]");

        foreach (var child in item.Children)
        {
            AddNode(node, child);
        }
    }

    private static void ShowMessage(string message)
    {
        AnsiConsole.MarkupLine($"[green]{Markup.Escape(message)}[/]");

        AnsiConsole.MarkupLine("[grey]Press any key to continue...[/]");

        Console.ReadKey(true);
    }
}