using ConsoleConfigurationLibrary.Classes;
using Dapper;
using MenuImageSampleApp.Models;
using Microsoft.Data.SqlClient;

namespace MenuImageSampleApp.Classes;

/// <summary>
/// Provides methods for retrieving and managing menu items associated with categories.
/// </summary>
/// <remarks>
/// This class is responsible for interacting with the database to fetch category-related data
/// and converting it into menu items that can be displayed and interacted with in the application.
/// </remarks>
public sealed class MenuItemRepository
{

    /// <summary>
    /// Retrieves a list of menu items associated with categories from the database.
    /// </summary>
    /// <returns>
    /// A list of <see cref="MenuItem"/> objects representing the categories, 
    /// including an additional "Exit" menu item for application termination.
    /// </returns>
    /// <remarks>
    /// This method executes a database query to fetch category data, maps the results to 
    /// <see cref="MenuItem"/> objects, and appends an "Exit" menu item with a predefined action.
    /// </remarks>
    public static List<MenuItem> GetCategoryMenuItems()
    {
        const string sql = """
                           SELECT
                               Id,
                               Name
                           FROM [WorkingImages].[dbo].[Categories];
                           """;

        using var connection = new SqlConnection(AppConnections.Instance.MainConnection);

        var categories = connection.Query<CategoryDto>(sql);

        var list = categories
            .Select(c => new MenuItem
            {
                Id = c.Id,
                Text = c.Name,
                Action = OnMenuItemSelected
            })
            .ToList();

        list.Add(new MenuItem
        {
            Id = 0,
            Text = "Exit",
            Action = _ => Environment.Exit(0)
        });

        return list;
    }

    /// <summary>
    /// Handles the selection of a menu item by exporting the photo associated with the specified category ID.
    /// </summary>
    /// <param name="categoryId">The identifier of the category whose photo should be exported.</param>
    /// <remarks>
    /// This method utilizes the <see cref="CategoryPhotoExporter.ExportPhotoToFile(int, string)"/> 
    /// method to export the photo associated with the given category ID to a predefined directory.
    /// </remarks>
    private static void OnMenuItemSelected(int categoryId)
    {
        CategoryPhotoExporter.ExportPhotoToFile(categoryId, "Photos");
    }
}