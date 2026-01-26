using Spectre.Console;
using SpectreConsoleMenuApp.Models;

namespace SpectreConsoleMenuApp.Classes;
internal class MenuOperations
{

    public static SelectionPrompt<Models.MenuItem> SelectionPrompt()
    {
        SelectionPrompt<MenuItem> menu = new()
        {
            HighlightStyle = new Style(Color.White, Color.Blue, Decoration.None)
        };

        menu.Title("[cyan]Select[/] :house:");
        menu.EnableSearch();
        menu.AddChoices(new List<MenuItem>()
        {
            new ()
            {
                Id = 1,
                Text = "Model Format Custom",
                Action = Operations.ModelFormatCustomSample
            },
            new ()
            {
                Id = 2,
                Text = "Test NextValue method",
                Action = Operations.NextValueSample
            },
            new ()
            {
                Id = -1,
                Text = "Exit",
                Action = Operations.ExitApplication
            },
        });

        return menu;

    }

}

