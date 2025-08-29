// ReSharper disable RedundantIfElseBlock

namespace PromptFilesExamplesApp1.Classes;

public static class StringExtensions
{
    /// <summary>
    /// Extracts a substring from the input string, either up to the first period (inclusive)
    /// or up to the first three words, whichever comes first.
    /// </summary>
    /// <param name="input">The input string to process.</param>
    /// <returns>
    /// A substring of the input string that ends at the first period (if present),
    /// or contains the first three words if no period is found. Returns an empty string
    /// if the input is null, empty, or consists only of whitespace.
    /// </returns>
    public static string UpToFirstPeriodOrThreeWords(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return string.Empty;
        }

        var periodIndex = input.IndexOf('.');

        if (periodIndex >= 0)
        {
            return input[..(periodIndex + 1)];
        }
        else
        {
            var words = input.Split([' '], StringSplitOptions.RemoveEmptyEntries);
            return string.Join(" ", words.Take(3));
        }
    }

}

