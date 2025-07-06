using System.Text.RegularExpressions;

namespace ReSharperExamples.Models;
public class Customer : Person, ICustomer
{

    public int CustomerId { get; set; }

    public List<Address> Addresses { get; set; }

}

public class Person : IPerson
{

    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }


    public required DateOnly BirthDate { get; set; }
}

public interface IPerson
{

    int Id { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
}

public interface ICustomer
{
    int CustomerId { get; set; }
}
public interface IAddress
{
    int Id { get; set; }
    int CustomerId { get; set; }
    string Street { get; set; }
    string City { get; set; }
    string State { get; set; }
    string ZipCode { get; set; }
    string Country { get; set; }
    string Phone { get; set; }
}
public class Address : IAddress
{
    public int Id { get; set; }
    public required int CustomerId { get; set; }
    public required string Street { get; set; }
    public required string City { get; set; }
    public string State { get; set; }
    
    public required string ZipCode { get; set; }
    public required string Country { get; set; }
    public required string Phone { get; set; }


    public string FullAddress => $"{Street}, {City}, {State}, {ZipCode}, {Country}";
}
public class Helpers
{

    public static List<string> GetStateAbbreviations() =>
    [
        "AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", "FL", "GA",
        "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD",
        "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ",
        "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC",
        "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY"
    ];
}

public static partial class StringExtensions
{


    public static string CapitalizeFirstLetter(this string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return input;

        return char.ToUpper(input[0]) + input.AsSpan(1).ToString();
    }



    public static string TrimLastCharacter(this string sender)
        => string.IsNullOrWhiteSpace(sender) ?
            sender :
            sender[..^1];


    public static string RemoveExtraSpaces(this string source, bool trimEnd = false)
    {
        var result = ExtraSpacesRegex().Replace(source, " ");
        return trimEnd ? result.TrimEnd() : result;
    }

    public static string ReplaceLast(this string source, string find, string replace)
    {
        var index = source.LastIndexOf(find, StringComparison.OrdinalIgnoreCase);
        return index == -1 ?
            source :
            source.Remove(index, find.Length).Insert(index, replace);
    }


    [GeneratedRegex(@"\s{2,}", RegexOptions.None)]
    private static partial Regex ExtraSpacesRegex();
}