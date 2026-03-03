
namespace ExportedVisualStudioExtensionsApp.Controls;

/// <summary>
/// Represents a customized <see cref="BindingNavigator"/> control tailored for managing 
/// data-binding operations in the application. This control disables the default functionality 
/// for adding and deleting items, ensuring a more controlled user experience.
/// </summary>
public sealed class CoreBindingNavigator : BindingNavigator
{
    public CoreBindingNavigator()
    {
        AddStandardItems();

        if (AddNewItem != null)
        {
            AddNewItem.Enabled = false;
            AddNewItem.Visible = false;
        }

        if (DeleteItem != null)
        {
            DeleteItem.Enabled = false;
            DeleteItem.Visible = false;
        }
    }
}