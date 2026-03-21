using GetSettingFromAppSettings.Models;
using Microsoft.Extensions.Options;

namespace GetSettingFromAppSettings.Classes;

/// <summary>
/// Provides validation logic for the <see cref="LoggingSettings"/> configuration section.
/// </summary>
/// <remarks>
/// This class ensures that the required configuration sections and values for logging settings
/// are present and valid in the application's configuration.
/// </remarks>
public sealed class LoggingSettingsValidation : IValidateOptions<LoggingSettings>
{
    private readonly IConfiguration _configuration;

    public LoggingSettingsValidation(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Validates the <see cref="LoggingSettings"/> configuration section.
    /// </summary>
    /// <param name="name">
    /// The name of the options instance being validated. This parameter is optional and may be <c>null</c>.
    /// </param>
    /// <param name="options">
    /// The <see cref="LoggingSettings"/> instance to validate.
    /// </param>
    /// <returns>
    /// A <see cref="ValidateOptionsResult"/> indicating the success or failure of the validation.
    /// </returns>
    /// <remarks>
    /// This method checks for the existence of the required configuration sections and values
    /// within the "Logging" section of the application's configuration. If any required sections
    /// or values are missing or invalid, the method returns a failure result with a list of errors.
    /// </remarks>
    public ValidateOptionsResult Validate(string? name, LoggingSettings options)
    {
        List<string> errors = [];

        var loggingSection = _configuration.GetSection("Logging");
        if (!loggingSection.Exists())
        {
            errors.Add("Configuration section 'Logging' is missing.");
            return ValidateOptionsResult.Fail(errors);
        }

        var logLevelSection = _configuration.GetSection("Logging:LogLevel");
        if (!logLevelSection.Exists())
        {
            errors.Add("Configuration section 'Logging:LogLevel' is missing.");
            return ValidateOptionsResult.Fail(errors);
        }

        ValidateValue("Logging:LogLevel:Default", errors);
        ValidateValue("Logging:LogLevel:Microsoft.AspNetCore", errors);
        ValidateValue("Logging:LogLevel:Microsoft.EntityFrameworkCore.Database.Command", errors);

        return errors.Count > 0
            ? ValidateOptionsResult.Fail(errors)
            : ValidateOptionsResult.Success;
    }

    /// <summary>
    /// Validates the value of a specific configuration key and adds an error message to the provided list if the value is missing or empty.
    /// </summary>
    /// <param name="key">
    /// The configuration key to validate.
    /// </param>
    /// <param name="errors">
    /// A list to which error messages will be added if the validation fails.
    /// </param>
    /// <remarks>
    /// This method retrieves the value associated with the specified configuration key from the application's configuration.
    /// If the value is <c>null</c>, empty, or consists only of whitespace, an error message is added to the <paramref name="errors"/> list.
    /// </remarks>
    private void ValidateValue(string key, List<string> errors)
    {
        var value = _configuration[key];

        if (string.IsNullOrWhiteSpace(value))
        {
            errors.Add($"Configuration value '{key}' is missing or empty.");
        }
    }
}