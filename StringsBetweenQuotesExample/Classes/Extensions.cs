using System.Text.RegularExpressions;

namespace StringsBetweenQuotesExample.Classes;


internal static partial class Extensions
{
    /// <summary>
    /// Extracts all substrings enclosed in quotes from the provided string.
    /// </summary>
    /// <param name="sender">The input string to search for quoted substrings.</param>
    /// <returns>A list of substrings found between quotes in the input string.</returns>
    public static List<string> StringsBetweenQuotes(this string sender)
    {
        var matches = QuotesRegex().Matches(sender);

        var strings = new List<string>();
        foreach (Match match in matches)
        {
            strings.Add(match.Groups[0].Value);
        }

        return strings;
    }

    /// <summary>
    /// Extracts all substrings enclosed in quotes from the provided string, 
    /// removing the enclosing quotes from each substring.
    /// </summary>
    /// <param name="sender">The input string to search for quoted substrings.</param>
    /// <returns>A list of substrings found between quotes in the input string, 
    /// with the enclosing quotes removed.</returns>
    public static List<string> StringsBetweenQuotes1(this string sender)
    {
        var matches = QuotesRegex().Matches(sender);

        var strings = new List<string>();
        foreach (Match match in matches)
        {
            strings.Add(match.Value.Length > 2 ? match.Value[1..^1] : "");
        }

        return strings;
    }

    /// <summary>
    /// Extracts all substrings enclosed in quotes from the provided string, 
    /// optionally keeping the enclosing quotes in each substring.
    /// </summary>
    /// <param name="sender">The input string to search for quoted substrings.</param>
    /// <param name="keep">
    /// A boolean value indicating whether to keep the enclosing quotes:
    /// <c>true</c> to include the quotes in the result; <c>false</c> to exclude them.
    /// </param>
    /// <returns>
    /// A list of substrings found between quotes in the input string. 
    /// If <paramref name="keep"/> is <c>true</c>, the substrings will include the enclosing quotes; 
    /// otherwise, the quotes will be removed.
    /// </returns>
    public static List<string> StringsBetweenQuotes1(this string sender, bool keep)
    {
        var matches = QuotesRegex().Matches(sender);

        var strings = new List<string>();
        foreach (Match match in matches)
        {
            if (keep)
            {
                strings.Add(match.Groups[0].Value);
            }
            else
            {
                strings.Add(match.Value.Length > 2 ? match.Value[1..^1] : "");
            }
                
        }

        return strings;

    }

    public static List<string> StringsBetweenQuotes2(this string sender, bool keep)
    {
        var matches = QuotesRegex().Matches(sender);
        var strings = new List<string>(matches.Count);

        foreach (Match match in matches)
        {
            strings.Add(keep ? match.Groups[0].Value : match.Value.Length > 2 ? match.Value[1..^1] : "");
        }

        return strings;
    }


}



