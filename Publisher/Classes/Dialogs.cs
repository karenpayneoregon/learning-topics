using System;
using System.Windows.Forms;

namespace Publisher.Classes;

/// <summary>
/// Provides utility methods for displaying dialog boxes, such as confirmation dialogs, 
/// with customizable options for buttons, captions, and default selections.
/// </summary>
internal class Dialogs
{
    /// <summary>
    /// Displays a dialog box to ask a question with customizable buttons and default selection.
    /// </summary>
    /// <param name="owner">The owner form of the dialog.</param>
    /// <param name="caption">The text to display in the dialog's caption.</param>
    /// <param name="heading">The text to display as the dialog's heading.</param>
    /// <param name="yesText">The text to display on the "Yes" button. Defaults to "Yes".</param>
    /// <param name="noText">The text to display on the "No" button. Defaults to "No".</param>
    /// <param name="defaultButton">
    /// Specifies the default button for the dialog. 
    /// Use <see cref="DialogResult.Yes"/> for the "Yes" button or <see cref="DialogResult.No"/> for the "No" button. Defaults to <see cref="DialogResult.No"/>.
    /// </param>
    /// <returns>
    /// Returns <c>true</c> if the "Yes" button is clicked; otherwise, <c>false</c>.
    /// </returns>
    public static bool Question(Form owner, string caption, string heading, string yesText = "Yes", string noText = "No", DialogResult defaultButton = DialogResult.No )
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

        return (DialogResult)result.Tag == DialogResult.Yes;

    }

    /// <summary>
    /// Displays an informational dialog box with a specified heading and a single button.
    /// </summary>
    /// <param name="owner">The control that owns the dialog box.</param>
    /// <param name="heading">The text to display as the dialog's heading.</param>
    /// <param name="buttonText"> The text to display on the button. Defaults to "Ok". </param>
    public static void Information(Control owner, string heading, string buttonText = "Ok")
    {

        TaskDialogButton okayButton = new(buttonText);

        TaskDialogPage page = new()
        {
            Caption = "Information",
            SizeToContent = true,
            Heading = heading,
            Icon = TaskDialogIcon.Warning,
            Buttons = [okayButton]
        };

        TaskDialog.ShowDialog(owner, page);

    }

    /// <summary>
    /// Displays an error dialog box with the details of the specified exception.
    /// </summary>
    /// <param name="exception">The exception whose details are to be displayed in the dialog.</param>
    /// <param name="buttonText">
    /// The text to display on the button in the dialog. Defaults to "Okay".
    /// </param>
    public static void ErrorBox(Exception exception, string buttonText = "Okay")
    {

        TaskDialogButton singleButton = new(buttonText);

        var text = $"Encountered the following\n{exception.Message}";


        TaskDialogPage page = new()
        {
            Caption = "Error",
            SizeToContent = true,
            Heading = text,
            Icon = TaskDialogIcon.Error,
            Buttons = [singleButton]
        };

        TaskDialog.ShowDialog(page);

    }
}