using Microsoft.Extensions.Options;

namespace HelpDeskApplication.Classes;

/// <summary>
/// Provides validation logic for the <see cref="RequiredDirectories"/> options.
/// </summary>
/// <remarks>
/// This class implements the <see cref="IValidateOptions{TOptions}"/> interface to ensure that
/// the required directories specified in the <see cref="RequiredDirectories"/> configuration
/// are valid and exist on the file system.
/// </remarks>
public class DirectoryOptionsValidation : IValidateOptions<RequiredDirectories>
{
    /// <summary>
    /// Validates the specified <see cref="RequiredDirectories"/> options.
    /// </summary>
    /// <param name="name">
    /// The name of the options instance being validated. This parameter is optional and can be <c>null</c>.
    /// </param>
    /// <param name="options">
    /// The <see cref="RequiredDirectories"/> instance containing the configuration to validate.
    /// </param>
    /// <returns>
    /// A <see cref="ValidateOptionsResult"/> indicating whether the validation was successful or failed.
    /// Returns <see cref="ValidateOptionsResult.Success"/> if the validation passes, or
    /// <see cref="ValidateOptionsResult.Fail(string)"/> with an error message if the validation fails.
    /// </returns>
    /// <remarks>
    /// This method ensures that the directory specified in <see cref="RequiredDirectories.SecretsDirectory"/>
    /// exists on the file system. If the directory does not exist, the validation fails with an appropriate error message.
    /// </remarks>
    public ValidateOptionsResult Validate(string? name, RequiredDirectories options) =>
        !Directory.Exists(options.SecretsDirectory) ? 
            ValidateOptionsResult.Fail($"The directory '{options.SecretsDirectory}' does not exist.") : 
            ValidateOptionsResult.Success;
}