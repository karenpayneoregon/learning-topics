using System.ComponentModel.DataAnnotations;

namespace HelpDeskApplication.Classes;

/// <summary>
/// Provides a custom validation attribute to verify that a specified directory exists.
/// </summary>
public class DirectoryExistsAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is string path && !Directory.Exists(path))
        {
            return new ValidationResult($"The directory '{path}' does not exist.");
        }
        return ValidationResult.Success;
    }
}