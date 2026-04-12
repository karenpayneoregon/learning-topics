
using System.ComponentModel;

namespace ProcessOrdersApp.Controls;

/// <summary>
/// Represents a custom implementation of a <see cref="BindingNavigator"/> control 
/// with additional functionality for managing navigation and actions in a data-bound application.
/// </summary>
/// <remarks>
/// This class extends the standard <see cref="BindingNavigator"/> to include additional buttons 
/// such as "About" and "Current", and provides methods to enable or disable the default add and delete actions.
/// It is designed to simplify navigation and data manipulation in applications where the default 
/// <see cref="BindingNavigator"/> is not available or requires customization.
/// </remarks>
[DefaultProperty("BindingSource")]
public sealed class CoreBindingNavigator : BindingNavigator
{
    public CoreBindingNavigator()
    {
        AddStandardItems();

        Items.Add(new ToolStripSeparator());
        Items.Add(new ToolStripButton() { Name = "bindingNavigatorAboutItem", Text = "About" });
        Items.Add(new ToolStripButton() { Name = "bindingNavigatorCurrentItem", Text = "Current" });

        AddButtonEnable();
        RemoveButtonEnable();
    }

    /// <summary>
    /// Set Enable for add button
    /// </summary>
    public void AddButtonEnable(bool enable = false)
    {
        AddNewItem.Enabled = enable;
    }

    /// <summary>
    /// Set Enable for delete button
    /// </summary>
    public void RemoveButtonEnable(bool enable = false)
    {
        DeleteItem.Enabled = enable;
    }

    /// <summary>
    /// Remove default actions for delete and add buttons
    /// </summary>
    public void RemoveDefaultHandlers()
    {
        AddNewItem = null;
        DeleteItem = null;
    }

    /// <summary>
    /// Hide about button
    /// </summary>
    public void HideAboutButton()
    {
        AboutItemButton.Visible = false;
    }

    /// <summary>
    /// Show about button
    /// </summary>
    public void ShowAboutButton()
    {
        AboutItemButton.Visible = true;
    }

    /// <summary>
    /// Provide access to the add new button
    /// </summary>
    public ToolStripItem AddItemButton => Items["bindingNavigatorAddNewItem"]!;
    /// <summary>
    /// Provides access to the delete current row
    /// </summary>
    public ToolStripItem DeleteItemButton => Items["bindingNavigatorDeleteItem"]!;
    public ToolStripItem AboutItemButton => Items["bindingNavigatorAboutItem"]!;
    public ToolStripItem CurrentItemButton => Items["bindingNavigatorCurrentItem"]!;
}
