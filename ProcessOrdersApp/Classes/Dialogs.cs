namespace ProcessOrdersApp.Classes;
internal class Dialogs
{
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

        var iconBitmap = IconBitmap();

        TaskDialogPage page = new()
        {
            Caption = "Information",
            SizeToContent = true,
            Heading = heading,
            Icon = new TaskDialogIcon(iconBitmap),
            Footnote = new TaskDialogFootnote() { Text = "Code sample by Karen Payne" },
            Buttons = [okayButton]
        };

        TaskDialog.ShowDialog(owner, page);
    }

    /// <summary>
    /// Creates a <see cref="Bitmap"/> object from an embedded resource.
    /// </summary>
    /// <returns>
    /// A <see cref="Bitmap"/> object created from the embedded resource.
    /// </returns>
    /// <remarks>
    /// This method reads the resource <c>exclamation24</c> from the application's resources,
    /// converts it into a <see cref="Bitmap"/>, and returns the resulting object.
    /// </remarks>
    private static Bitmap IconBitmap()
    {
        Bitmap bitmap;
        using var ms = new MemoryStream(Properties.Resources.exclamation24);
        using var temp = new Bitmap(ms);
        bitmap = new Bitmap(temp);

        return bitmap;
    }
}
