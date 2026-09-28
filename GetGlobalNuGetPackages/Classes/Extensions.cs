using System.Diagnostics;
using System.Text.RegularExpressions;

namespace GetGlobalNuGetPackages.Classes;
internal static partial class Extensions
{
    /// <summary>
    /// Converts a boolean value to its corresponding text representation.
    /// </summary>
    /// <param name="value">The boolean value to convert.</param>
    /// <returns>Returns "Yes" if the value is <c>true</c>; otherwise, returns "No".</returns>
    [DebuggerStepThrough]
    public static string ToText(this bool value) => value ? "Yes" : "No";

    /// <summary>
    /// Splits the given string into separate words based on the case of the letters.
    /// </summary>
    /// <param name="sender">The string to split.</param>
    /// <returns>A new string with the words separated by spaces.</returns>
    [DebuggerStepThrough]
    public static string SplitCase(this string sender) =>
        string.Join(" ", CaseRegEx().Matches(sender)
            .Select(m => m.Value));

    [GeneratedRegex(@"([A-Z][a-z]+)")]
    private static partial Regex CaseRegEx();
}
