

using MenuSampleApp.Models;
using Spectre.Console;

namespace MenuSampleApp.Classes;

class MenuOperations
{
    
    /// <summary>
    /// Creates and configures a selection prompt for the main menu of the application.
    /// </summary>
    /// <returns>
    /// A <see cref="Spectre.Console.SelectionPrompt{T}"/> of type <see cref="MenuSampleApp.Models.MenuItem"/> 
    /// containing the menu options for the application.
    /// </returns>
    /// <remarks>
    /// The selection prompt includes options for viewing the table count in the NorthWind database, 
    /// viewing categories, and exiting the application. Each menu item is associated with a specific action.
    /// </remarks>
    public static SelectionPrompt<MenuItem> MainSelectionPrompt()
    {
        SelectionPrompt<MenuItem> menu = new()
        {
            HighlightStyle = new Style(Color.White, Color.Blue, Decoration.None)
        };

        menu.Title("[cyan]Select[/]");
        menu.EnableSearch();
        menu.AddChoices(new List<MenuItem>()
        {
            new () 
            {
                Id = 1, 
                Text = "Table count for NorthWind2024",
                Action = Operations.NorthWindTableCount 
            },
            new () 
            {
                Id = 2, 
                Text = "View categories", 
                Action = Operations.ViewCategories
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