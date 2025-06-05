using System.Text;
using PrintMembersSamples.Classes;
using PrintMembersSamples.Interfaces;

namespace PrintMembersSamples.Models;

public record Person(string FirstName, string LastName, DateOnly BirthDate, string SSN, string[] PhoneNumbers) : ITaxpayer
{
    public override string ToString()
    {
        var builder = new StringBuilder();

        var person = (BirthDate.Year.IsBetween(1980,1985))
            ? this with { BirthDate = default }
            : this;

        person.PrintMembers(builder);

        return builder.ToString();
    }

    protected virtual bool PrintMembers(StringBuilder sb)
    {
        sb.Append($"{FirstName,-10}{LastName, -10}{SSN.MaskSsn(), -13}{BirthDate,-12:MM/dd/yyyy}");

        if (!(PhoneNumbers?.Length > 0))
        {
            sb.Append("None");
            return true;
        }

        sb.Append(string.Join(", ", PhoneNumbers));
        sb.Append("");

        return true;
    }

}

