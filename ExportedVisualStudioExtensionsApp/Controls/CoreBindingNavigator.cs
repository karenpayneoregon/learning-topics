
namespace ExportedVisualStudioExtensionsApp.Controls;

/// <summary>
/// Represents a specialized <see cref="BindingNavigator"/> control with customized behavior 
/// for managing data-binding operations. This class disables the default functionality 
/// of adding and deleting items.
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