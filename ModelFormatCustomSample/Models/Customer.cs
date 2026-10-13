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
            "A" or "Age" => $"{BirthDay.GetAge()}",
            "IFl" or "G" => $"{Id,-5}{FirstName} {LastName}",
            "FL" => $"{FirstName} {LastName}",
            "B" => $"{BirthDay:O}",
            "I" => $"{Id}",
            _ => $"{Id,-3}{FirstName} {LastName}, {BirthDay}"
        };

    public override string ToString()
        => $"{FirstName} {LastName}, age {BirthDay.GetAge()}";

    public void Deconstruct(out int id, out string firstName, out string lastName, out DateOnly birth)
    {
        id = Id;
        firstName = FirstName;
        lastName = LastName;
        birth = BirthDay;
    }
}