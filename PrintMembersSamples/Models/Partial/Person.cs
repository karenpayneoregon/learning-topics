using System.Text;
using PrintMembersSamples.Classes;

// ReSharper disable once CheckNamespace
namespace PrintMembersSamples.Models;
public partial record Person
{
    public override string ToString()
    {
        var builder = new StringBuilder();
        
        var person = this;

        /*
         * Commented code evaluates whether the `BirthDate.Year` of the `Person` instance falls
         * between 1980 and 1985 (inclusive) using the `IsBetween` extension method. If the condition is true,
         * it creates a new `Person` instance with the same properties as the current instance
         * (`this`) but with `BirthDate` set to its default value. Otherwise, it retains the current
         * instance (`this`). The resulting `Person` instance is then assigned to the `person` variable.
         */
        if (!System.Diagnostics.Debugger.IsAttached)
        {
            //person = (BirthDate.Year.IsBetween(1980, 1985))
            //    ? this with { BirthDate = default }
            //    : this;

            // Hide the birthdate the outside of the debugger
            person = person with
            {
                BirthDate = default
            };
        }
        
        person.PrintMembers(builder);

        return builder.ToString();
    }

    protected virtual bool PrintMembers(StringBuilder sb)
    {
        if (System.Diagnostics.Debugger.IsAttached)
        {
            sb.Append($"{FirstName,-10}{LastName,-10}{SSN,-13}{BirthDate,-12:MM/dd/yyyy}");
        }
        else
        {
            sb.Append($"{FirstName,-10}{LastName,-10}{SSN.MaskSsn(),-13}{BirthDate,-12:MM/dd/yyyy}");
        }


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
