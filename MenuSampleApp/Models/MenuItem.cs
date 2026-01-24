namespace MenuSampleApp.Models;

/// <summary>
/// Represents a menu item in the application.
/// </summary>
/// <remarks>
/// This class is used to define individual menu options, each with an identifier, display text, 
/// and an associated action to be executed when the menu item is selected.
/// </remarks>
public class MenuItem
{

    public int Id { get; set; }
    public required string Text { get; set; }
    public required Action Action { get; set; }
    public override string ToString() => Text;
}