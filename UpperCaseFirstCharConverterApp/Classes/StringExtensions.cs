namespace UpperCaseFirstCharConverterApp.Classes;
public static class StringExtensions
{
    /// <summary>
    /// Converts the first character of the given string to uppercase.
    /// </summary>
    /// <param name="sender">The input string.</param>
    /// <returns>
    /// A new string with the first character converted to uppercase 
    /// </returns>
    public static string CapitalizeFirstLetter(this string sender)
        => string.IsNullOrEmpty(sender) ? sender : $"{char.ToUpper(sender[0])}{sender[1..].ToLower()}";
}
