using ModelFormatCustomSample.Classes;

namespace ModelFormatCustomSample.Models;
public record Customer(int Id, string FirstName, string LastName, DateOnly BirthDay) : IFormattable
{
    public int Id { get; set; } = Id;
    public string FirstName { get; set; } = FirstName;
    public string LastName { get; set; } = LastName;
    public DateOnly BirthDay { get; set; } = BirthDay;
    public string ToString(string? format, IFormatProvider? _)
        => format switch
        {
            "A" => $"{BirthDay.GetAge()}",
            "F" => $"{Id,-5}{FirstName} {LastName}",
            "N" => $"{FirstName} {LastName}",
            "B" => $"{BirthDay}",
            "I" => $"{Id}",
            _ => $"{Id,-3}{BirthDay} {LastName}, {BirthDay}"
        };
    /// <summary>
    /// Returns a string representation of the customer, including their full name and age.
    /// </summary>
    /// <returns>A <see cref="string"/> containing the customer's full name and age.</returns>
    public override string ToString()
        => $"{FirstName} {LastName}, age {BirthDay.GetAge()}";

    internal void Deconstruct(out string firstName, out string lastName, out DateOnly birth)
    {
        firstName = FirstName;
        lastName = LastName;
        birth = BirthDay;
    }
}