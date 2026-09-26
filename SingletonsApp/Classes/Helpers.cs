using System.Text.RegularExpressions;

namespace SingletonsApp.Classes;


public partial class Helpers
{

    /// <summary>
    /// Updates the input string by incrementing the numeric portion at the end of the string.
    /// </summary>
    /// <param name="sender">
    /// A reference to the input string containing a numeric portion at the end. 
    /// The numeric portion will be incremented, and the updated string will be assigned back to this parameter.
    /// </param>
    /// <param name="incrementBy">
    /// The value by which to increment the numeric portion of the input string. Defaults to 1.
    /// </param>
    /// <exception cref="System.FormatException">
    /// Thrown if the numeric portion of the input string cannot be parsed as a valid number.
    /// </exception>
    /// <exception cref="System.ArgumentNullException">
    /// Thrown if the <paramref name="sender"/> is <c>null</c>.
    /// </exception>
    public static void NextValue(ref string sender, int incrementBy = 1)
    {
        string value = NumbersPattern().Match(sender).Value;

        sender = sender[..^value.Length] + (long.Parse(value) + incrementBy)
            .ToString().PadLeft(value.Length, '0');
    }

    [GeneratedRegex("[0-9]+$")]
    private static partial Regex NumbersPattern();
}

