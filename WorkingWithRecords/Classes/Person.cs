using System.Text;

namespace WorkingWithRecords.Classes;

public partial record Person(string FirstName, string LastName, DateOnly BirthDate, string[] PhoneNumbers)
{


    //public override string ToString()
    //{
    //    var builder = new StringBuilder();

    //    (this with { BirthDate = default }).PrintMembers(builder);

    //    return builder.ToString();
    //}

    public override string ToString()
    {
        var builder = new StringBuilder();

        var person = (FirstName == "Karen" && LastName == "Payne")
            ? this with { BirthDate = default }
            : this;

        person.PrintMembers(builder);

        return builder.ToString();
    }
}