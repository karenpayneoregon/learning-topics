using System.Globalization;

namespace BirthDaysApp.Models;

/// <summary>
/// Represents a view model for a person, including their first name, last name, birthdate, and age.
/// </summary>
/// <remarks>
/// This record is used to encapsulate and format personal information for display or processing.
/// It implements <see cref="IFormattable"/> to provide custom string representations based on specified formats.
/// </remarks>
public record PersonViewModel(string FirstName, string LastName, DateOnly BirthDate, int Age) : IFormattable
{
    public string ToString(string? format, IFormatProvider? formatProvider) =>
        format switch
        {
            "F" or "FullName" or null or "" => $"{FirstName} {LastName}",
            "A" or "Age" => Age.ToString(GlobalCulture.InvariantCulture),
            _ => $"{FirstName} {LastName}"
        };
}