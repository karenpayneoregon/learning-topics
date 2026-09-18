// ReSharper disable NonReadonlyMemberInGetHashCode

using CapitalizeFirstLetterSample.Classes;

#pragma warning disable CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
namespace CapitalizeFirstLetterSample.Models;

/// <summary>
/// Represents a person with properties for identification, name, and birthdate.
/// </summary>
public class Person 
{
    public int Id { get; set; }
    
    public string? FirstName
    {
        get;
        set => field = value.CapitalizeFirstLetter() ?? string.Empty;
    }
    public string? LastName
    {
        get;
        set => field = value.CapitalizeFirstLetter() ?? string.Empty;
    }
    
    public DateOnly BirthDate { get; set; }
    
    public override string ToString() => $"{Id,-5}{FirstName} {LastName} {BirthDate:MM/dd/yyyy}";
}
