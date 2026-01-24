using MenuImageSampleApp.Models;
using Spectre.Console;

namespace MenuImageSampleApp.Classes;

public static class MenuOperations
{
    /// <summary>
    /// Creates a selection prompt for displaying and selecting menu items.
    /// </summary>
    /// <returns>
    /// A <see cref="Spectre.Console.SelectionPrompt{T}"/> of type <see cref="MenuItem"/> 
    /// configured with a title, search functionality, and a list of menu items.
    /// </returns>
    /// <remarks>
    /// This method initializes a selection prompt with a custom highlight style, 
    /// sets its title, enables search functionality, and populates it with menu items 
    /// retrieved from the <see cref="MenuItemRepository.GetCategoryMenuItems"/> method.
    /// </remarks>
    public static SelectionPrompt<MenuItem> SelectionPrompt()
    {
        SelectionPrompt<MenuItem> menu = new()
        {
            HighlightStyle = new Style(Color.White, Color.Blue, Decoration.None)
        };

        menu.Title("[cyan]Select[/]");
        menu.EnableSearch();
        menu.AddChoices(MenuItemRepository.GetCategoryMenuItems());

        return menu;
    }
}
