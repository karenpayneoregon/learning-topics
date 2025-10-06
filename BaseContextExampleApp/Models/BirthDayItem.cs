namespace BaseContextExampleApp.Models;

/// <summary>
/// Represents a record containing details about an individual's birthday.
/// This includes their unique identifier (ID), first name, and last name.
/// Additionally, it may include an optional birthdate, which is represented
/// using the DateOnly type to store only the date component without time.
/// This record is designed to encapsulate basic personal information
/// relevant to birthday tracking or management scenarios.
/// </summary>
public record BirthDayItem(int Id, string FirstName, string LastName, DateOnly? BirthDate);