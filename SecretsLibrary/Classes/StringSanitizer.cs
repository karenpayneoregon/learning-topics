using System.Diagnostics;
using System.Text.RegularExpressions;

namespace SecretsLibrary.Classes;
public static partial class StringSanitizer
{

    /// <summary>
    /// Removes quotes and line breaks (carriage return and line feed) from the specified input string.
    /// </summary>
    /// <param name="input">The input string to sanitize. Can be <c>null</c> or empty.</param>
    /// <returns>
    /// A sanitized string with quotes and line breaks removed. 
    /// If the input is <c>null</c> or empty, the same value is returned.
    /// </returns>
    /// <remarks>Copilot change from using an 'if' statement to ternary operator</remarks>
    public static string StripQuotesAndBreaks(string input)
    {
        return string.IsNullOrEmpty(input) ? input : QuotesAndBreaksRegex().Replace(input, string.Empty);
    }

    /// <summary>
    /// Removes extra whitespace from the specified input string by replacing multiple consecutive 
    /// whitespace characters with a single space and trimming leading and trailing whitespace.
    /// </summary>
    /// <param name="input">The input string to process. Can be <c>null</c> or empty.</param>
    /// <returns>
    /// A string with extra whitespace removed. If the input is <c>null</c> or empty, the same value is returned.
    /// </returns>
    /// <remarks>
    /// Written by Copilot, this method uses a stack-allocated span to efficiently process the input string
    /// </remarks>
    [DebuggerStepThrough]
    public static string RemoveExtraWhitespace(this string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        Span<char> result = stackalloc char[input.Length];
        var resultIndex = 0;
        var isWhitespace = false;

        foreach (var currentChar in input)
        {
            if (char.IsWhiteSpace(currentChar))
            {
                if (!isWhitespace)
                {
                    result[resultIndex++] = ' ';
                    isWhitespace = true;
                }
            }
            else
            {
                result[resultIndex++] = currentChar;
                isWhitespace = false;
            }
        }

        return result[..resultIndex].ToString().Trim();
    }

    [GeneratedRegex(@"\\[uU]0022|\""|\r|\n")]
    private static partial Regex QuotesAndBreaksRegex();
}