namespace PartialSamples1.Classes.Extensions;
public static class StringExtensions
{
    /// <summary>
    /// Capitalizes the first letter of the given string.
    /// </summary>
    /// <param name="input">The string to capitalize.</param>
    /// <returns>A new string with the first letter capitalized. If the input is null or empty, the original string is returned.</returns>
    public static string CapitalizeFirstLetter(this string input) 
        => string.IsNullOrWhiteSpace(input) ? 
            input : char.ToUpper(input[0]) + input.AsSpan(1).ToString();
}
