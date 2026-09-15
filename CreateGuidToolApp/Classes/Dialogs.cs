namespace CreateGuidToolApp.Classes;


/// <summary>
/// Provides utility methods for displaying various types of dialogs, such as informational
/// and question dialogs, in a Windows Forms application.
/// </summary>
/// <remarks>
/// Icons for resources are in this project's asset folder
/// </remarks>
internal class Dialogs
{

    /// <summary>
    /// Displays a question dialog with customizable buttons and returns the user's choice.
    /// </summary>
    /// <param name="owner">
    /// The control or form that owns the dialog. This determines the dialog's parent window.
    /// </param>
    /// <param name="caption">
    /// The caption text to display in the title bar of the dialog.
    /// </param>
    /// <param name="heading">
    /// The heading text to display in the dialog.
    /// </param>
    /// <param name="yesText">
    /// The text to display on the "Yes" button. Defaults to "Yes" if not specified.
    /// </param>
    /// <param name="noText">
    /// The text to display on the "No" button. Defaults to "No" if not specified.
    /// </param>
    /// <param name="defaultButton">
    /// Specifies which button is selected by default when the dialog is displayed.
    /// Defaults to <see cref="DialogResult.No"/>.
    /// </param>
    /// <returns>
    /// <c>true</c> if the user selects the "Yes" button; otherwise, <c>false</c>.
    /// </returns>
    public static bool Question(Control owner, string caption, string heading, string yesText = "Yes", string noText = "No", DialogResult defaultButton = DialogResult.No)
    {

        TaskDialogButton yesButton = new(yesText) { Tag = DialogResult.Yes };
        TaskDialogButton noButton = new(noText) { Tag = DialogResult.No };

        TaskDialogButtonCollection buttons = new();

        if (defaultButton == DialogResult.Yes)
        {
            buttons.Add(yesButton);
            buttons.Add(noButton);
        }
        else
        {
            buttons.Add(noButton);
            buttons.Add(yesButton);
        }

        TaskDialogPage page = new()
        {
            Caption = caption,
            SizeToContent = true,
            Heading = heading,
            Icon = new TaskDialogIcon(Properties.Resources.question32),
            Buttons = buttons
        };


        var result = TaskDialog.ShowDialog(owner, page);

        return (DialogResult)result.Tag! == DialogResult.Yes;

    }
    
    /// <summary>
    /// Displays an informational dialog with a specified heading and an optional button text.
    /// </summary>
    /// <param name="owner">
    /// The control or form that owns the dialog. This determines the dialog's parent window.
    /// </param>
    /// <param name="heading">
    /// The heading text to display in the dialog.
    /// </param>
    /// <param name="buttonText">
    /// The text to display on the dialog's button. Defaults to "Ok" if not specified.
    /// </param>
    public static void Information(Control owner, string heading, string buttonText = "Ok")
    {
        
        TaskDialogButton okayButton = new(buttonText);

        TaskDialogPage page = new()
        {
            Caption = "Information",
            SizeToContent = true,
            Heading = heading,
            Icon = new TaskDialogIcon(Properties.Resources.Explaination),
            Footnote = new TaskDialogFootnote() { Text = "Code sample by Karen Payne" },
            Buttons = [okayButton]
        };

        TaskDialog.ShowDialog(owner, page);

    }
    
    /// <summary>
    /// Displays an error dialog with a specified exception message and an optional button text.
    /// </summary>
    /// <param name="owner">
    /// The control or form that owns the dialog. This determines the dialog's parent window.
    /// </param>
    /// <param name="exception">
    /// The exception whose message will be displayed in the dialog.
    /// </param>
    /// <param name="buttonText">
    /// The text to display on the dialog's button. Defaults to "See log file" if not specified.
    /// </param>
    public static void DisplayError(Control owner, Exception exception, string buttonText = "OK")
    {

        TaskDialogButton singleButton = new(buttonText);

        var text = $"Encountered the following\n{exception.Message}";


        TaskDialogPage page = new()
        {
            Caption = "Information",
            SizeToContent = true,
            Heading = text,
            Icon = TaskDialogIcon.Error,
            Buttons = [singleButton]
        };

        TaskDialog.ShowDialog(owner, page);

    }
    
    
    /// <summary>
    /// Converts an array of bytes representing image data into a <see cref="Bitmap"/> object.
    /// </summary>
    /// <param name="imageData">
    /// The byte array containing the image data to be converted.
    /// </param>
    /// <returns>
    /// A <see cref="Bitmap"/> object created from the provided byte array.
    /// </returns>
    public static Bitmap BytesToBitmap(byte[] imageData)
    {
        using var ms = new MemoryStream(imageData);
        return new Bitmap(ms);
    }
}
