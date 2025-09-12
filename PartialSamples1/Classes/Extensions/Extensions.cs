#nullable disable
namespace PartialSamples1.Classes.Extensions;

/// <summary>
/// Provides extension methods for string manipulation and validation.
/// </summary>
public static partial class Extensions
{
    
    /// <summary>
    /// Mask SSN
    /// </summary>
    /// <param name="ssn">Valid SSN</param>
    /// <param name="digitsToShow">How many digits to show on right which defaults to 4</param>
    /// <param name="maskCharacter">Character to mask with which defaults to X</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>    
    public static partial string MaskSsn(this string ssn, int digitsToShow = 4, char maskCharacter = 'X');

    /// <summary>
    /// Capitalizes the first letter of the given string.
    /// </summary>
    /// <param name="input">The string to capitalize.</param>
    /// <returns>A new string with the first letter capitalized. If the input is null or empty, the original string is returned.</returns>
    public static partial string CapitalizeFirstLetter(this string? input);
    public static partial bool IsInteger(this string sender);
    public static partial bool IsNotInteger(this string sender);
}
