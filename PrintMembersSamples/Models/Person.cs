using PrintMembersSamples.Interfaces;

namespace PrintMembersSamples.Models;

/// <summary>
/// Represents a person with basic identifying information and contact details.
/// </summary>
/// <param name="FirstName">The first name of the person.</param>
/// <param name="LastName">The last name of the person.</param>
/// <param name="BirthDate">The birthdate of the person.</param>
/// <param name="SSN">The Social Security Number (SSN) of the person.</param>
/// <param name="PhoneNumbers">An array of phone numbers associated with the person.</param>
public partial record Person(
    string FirstName, 
    string LastName, 
    DateOnly BirthDate, 
    string SSN,
    string UserName,
    string Password,
    string[] PhoneNumbers) : ITaxpayer { }