using System.Text.RegularExpressions;

namespace StringsBetweenQuotesExample.Classes;
internal static partial class Extensions
{
    [GeneratedRegex("([\"'])(?:(?=(\\\\?))\\2.)*?\\1")]
    public static partial Regex QuotesRegex();
}
