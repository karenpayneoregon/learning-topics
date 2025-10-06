namespace BaseContextExampleApp.Models;

/// <summary>
/// Represents a record containing personal details such as first name, last name, 
/// and an optional birthdate.
/// </summary>
/// <param name="FirstName">The first name of the individual.</param>
/// <param name="LastName">The last name of the individual.</param>
/// <param name="BirthDate">The optional birthdate of the individual.</param>
public record Values(string FirstName, string LastName, DateOnly? BirthDate);