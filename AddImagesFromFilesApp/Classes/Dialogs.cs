
namespace AddImagesFromFilesApp.Classes;
internal class Dialogs
{
 
    /// <summary>
    /// Displays an informational dialog with a specified heading, text, and button text.
    /// </summary>
    /// <param name="owner">The parent control that owns the dialog.</param>
    /// <param name="heading">The heading text displayed in the dialog.</param>
    /// <param name="text">The main content text displayed in the dialog.</param>
    /// <param name="buttonText">The text displayed on the button. Defaults to "Ok".</param>
    public static void Information(Control owner, string heading, string text, string buttonText = "Ok")
    {

        TaskDialogButton okayButton = new(buttonText);

        TaskDialogPage page = new()
        {
            Caption = "Information",
            SizeToContent = true,
            Heading = heading,
            Footnote = new TaskDialogFootnote() { Text = "Code sample by Karen Payne" },
            Text = text,
            Icon = new TaskDialogIcon(Properties.Resources.blueInformation_32),
            Buttons = [okayButton]
        };

        TaskDialog.ShowDialog(owner, page);

    }

}
