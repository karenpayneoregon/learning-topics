using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace SpectreConsoleMenuApp.Classes.Utilities;
public partial class Helpers
{

    /// <summary>
    /// Generates the next value for a string containing a numeric sequence by incrementing the numeric part.
    /// </summary>
    /// <param name="sender">The input string containing a numeric sequence to be incremented.</param>
    /// <param name="incrementBy">
    /// The value by which to increment the numeric sequence. Defaults to 1 if not specified.
    /// </param>
    /// <returns>
    /// A new string where the numeric sequence in the input string is incremented by the specified value,
    /// with the numeric part padded to match the original length.
    /// </returns>
    /// <remarks>
    /// This method identifies the numeric sequence at the end of the input string and increments it by the specified value.
    /// The numeric part is padded with leading zeros to maintain its original length.
    /// </remarks>
    /// <example>
    /// Example usage:
    /// <code>
    /// string result = Helpers.NextValue("A1B2C23");
    /// Console.WriteLine(result); // Output: A1B2C24
    /// </code>
    /// </example>
    /// <exception cref="FormatException">
    /// Thrown when the numeric sequence in the input string cannot be parsed as a valid number.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the <paramref name="sender"/> parameter is null.
    /// </exception>
    public static string NextValue(string sender, int incrementBy = 1)
    {
        string value = NumbersPattern().Match(sender).Value;

        return sender[..^value.Length] + (long.Parse(value) + incrementBy)
            .ToString().PadLeft(value.Length, '0');
    }

    /// <summary>
    /// Builds and retrieves the application configuration.
    /// </summary>
    /// <returns>
    /// An <see cref="IConfiguration"/> instance representing the application configuration,
    /// loaded from the "appsettings.json" file located in the application's base directory.
    /// </returns>
    /// <remarks>
    /// This method uses the <see cref="ConfigurationBuilder"/> to load configuration settings
    /// from a JSON file named "appsettings.json". The file is required and changes to it
    /// will trigger a reload of the configuration.
    /// </remarks>
    /// <example>
    /// Example usage:
    /// <code>
    /// IConfiguration configuration = Helpers.GetConfiguration();
    /// string settingValue = configuration["SomeSettingKey"];
    /// </code>
    /// </example>
    public static IConfiguration GetConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        return builder.Build();
    }

    [GeneratedRegex("[0-9]+$")]
    private static partial Regex NumbersPattern();
}

