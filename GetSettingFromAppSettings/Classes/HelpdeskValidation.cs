using GetSettingFromAppSettings.Models;
using Microsoft.Extensions.Options;

namespace GetSettingFromAppSettings.Classes;

public sealed class HelpdeskValidation(IConfiguration configuration) : IValidateOptions<HelpDesk>
{
    /// <summary>
    /// Validates the <see cref="HelpDesk"/> configuration section.
    /// </summary>
    /// <param name="name">
    /// The name of the options instance being validated. This parameter is optional and may be <c>null</c>.
    /// </param>
    /// <param name="options">
    /// The <see cref="HelpDesk"/> instance to validate.
    /// </param>
    /// <returns>
    /// A <see cref="ValidateOptionsResult"/> indicating the success or failure of the validation.
    /// </returns>
    /// <remarks>
    /// This method ensures that the "HelpDesk" configuration section and its required values, such as
    /// "Phone" and "Email", are present and valid. If any required sections or values are missing or invalid,
    /// the method returns a failure result with a list of errors.
    /// </remarks>
    public ValidateOptionsResult Validate(string? name, HelpDesk options)
    {
        List<string> errors = [];

        var helpdeskSection = configuration.GetSection("HelpDesk");
        if (!helpdeskSection.Exists())
        {
            errors.Add("Configuration section 'HelpDesk' is missing.");
            return ValidateOptionsResult.Fail(errors);
        }

        var phoneSection = helpdeskSection.GetSection("Phone");
        if (!phoneSection.Exists())
        {
            errors.Add("Configuration section 'Helpdesk:Phone' is missing.");
        }

        var emailSection = helpdeskSection.GetSection("Email");
        if (!emailSection.Exists())
        {
            errors.Add("Configuration section 'Helpdesk:Email' is missing.");
        }

        ValidateValue("HelpDesk:Phone", errors);
        ValidateValue("HelpDesk:Email", errors);

        return errors.Count > 0
            ? ValidateOptionsResult.Fail(errors)
            : ValidateOptionsResult.Success;
    }

    private void ValidateValue(string key, List<string> errors)
    {
        var value = configuration[key];

        if (string.IsNullOrWhiteSpace(value))
        {
            errors.Add($"Configuration value '{key}' is missing or empty.");
        }
    }
}