using System.Text;

namespace WorkingWithRecords.Classes;

public partial record Person
{
    protected virtual bool PrintMembers(StringBuilder sb)
    {
        sb.Append($"FirstName = {FirstName}, LastName = {LastName}, Birth = {BirthDate:MM/dd/yyyy}");

        if (!(PhoneNumbers?.Length > 0)) return true;

        sb.Append(", PhoneNumbers: ");
        sb.Append(string.Join(", ", PhoneNumbers));
        sb.Append("");

        return true;
    }
}