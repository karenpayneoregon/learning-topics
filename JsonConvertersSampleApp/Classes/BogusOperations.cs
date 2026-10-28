using Bogus;
using Bogus.DataSets;
using Bogus.Extensions.UnitedStates;
using JsonConvertersSampleApp.Models;
using static Bogus.Randomizer;

namespace JsonConvertersSampleApp.Classes;

/// <summary>
/// Used to create consistent data
/// </summary>
/// <remarks>
/// https://github.com/bchavez/Bogus/blob/master/Source/Bogus/DataSets/Name.cs
/// </remarks>
internal class BogusOperations
{

    /// <summary>
    /// Creates a consistent list of <see cref="Taxpayer"/>
    /// </summary>
    /// <param name="count">How many to create, if not passed the default is five</param>
    /// <returns>List of Taxpayers</returns>
    /// <remarks>
    /// Seed is set to 338 to ensure consistent results
    /// </remarks>
    public static List<Taxpayer> TaxpayerList(int count = 5)
    {

        Seed = new Random(339);
        var id = 1;

        var faker = new Faker<Taxpayer>()
            .RuleFor(t => t.Id, f => id++)
            .RuleFor(u => u.Gender, f => f.PickRandom<Gender>())
            .RuleFor(c => c.FirstName, (f, u) => 
                f.Name.FirstName((Name.Gender?)u.Gender))
            .RuleFor(t => t.LastName, f => f.Name.LastName())
            .RuleFor(t => t.Title, f => f.Name.JobTitle())
            .RuleFor(t => t.SSN, f => f.Person.Ssn())
            .RuleFor(t => t.PIN, f => f.Random.Replace("####"))
            .RuleFor(u => u.Gender, f => f.PickRandom<Gender>())
            .RuleFor(t => t.EmployerIdentificationNumber, f => f.Company.Ein())
            .RuleFor(t => t.BirthDate, f =>
                f.Date.BetweenDateOnly(new DateOnly(1899, 1, 1), new DateOnly(1999, 1, 1)))
            .RuleFor(t => t.StartDate, f =>
                f.Date.BetweenDateOnly(new DateOnly(2000, 1, 1), new DateOnly(2010, 1, 1)));


        return faker.Generate(count);

    }
}


public static class BogusExtensionsLocal    
{
    public static string Prefix2(this Name name, Name.Gender gender)
    {
        if (gender == Name.Gender.Male)
        {
            return name.Random.ArrayElement(["Mr.", "Dr."]);
        }

        return name.Random.ArrayElement(["Miss", "Ms.", "Mrs."]);
    }
}