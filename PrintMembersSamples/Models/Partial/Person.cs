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
        if (!Appsettings.Instance.RevealSensitiveInformation)
        {

            //person = (BirthDate.Year.IsBetween(1980, 1985))
            //    ? this with { BirthDate = default }
            //    : this;

            person = person with
            {
                BirthDate = default,
                UserName = "redacted",
                Password = "redacted"
            };
        }
        
        person.PrintMembers(builder);

        return builder.ToString();
    }

    /// <summary>
    /// Appends the string representation of the current <see cref="Person"/> instance's members to the specified
    /// <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="StringBuilder"/> to which the member information will be appended.</param>
    /// <returns>
    /// <c>true</c> if the operation was successful; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// This method formats and appends the member data of the <see cref="Person"/> instance, names, SSN, birthdate, 
    /// username, password, and phone numbers. If the debugger is attached, sensitive data is displayed in full; otherwise, 
    /// certain fields are masked or redacted for security.
    /// </remarks>
    protected virtual bool PrintMembers(StringBuilder builder)
    {
        if (Appsettings.Instance.RevealSensitiveInformation)
        {
            builder.Append($"{FirstName,-10}{LastName,-10}{SSN,-13}{BirthDate,-14:MM/dd/yyyy}{UserName,-10}{Password,-12}");
        }
        else
        {
            builder.Append($"{FirstName,-10}{LastName,-10}{SSN.MaskSsn(),-13}{BirthDate,-14:MM/dd/yyyy}{UserName,-10}{Password,-12}");
        }


        if (!(PhoneNumbers?.Length > 0))
        {
            builder.Append("None");
            return true;
        }

        builder.Append(string.Join(", ", PhoneNumbers));
        builder.Append("");

        return true;

    }
}
