using Bogus;
using BogusLibrary1.Interfaces;
using BogusLibrary1.Models;

namespace BogusLibrary1.Classes;

/// <summary>
/// Provides several methods for generating mocked data with emphasis on DateOnly, BetweenDateOnly and TimeOnly.
/// </summary>
/// <remarks>
/// See Program.cs for DI setup in BogusDateOnlyTimeOnlyApp.
/// </remarks>
public class MockedData : IMockedData
{
    /// <summary>
    /// Generate a list of customers with random time values and dates between two dates.
    /// </summary>
    /// <param name="count">How many customers to generate</param>
    /// <returns>Customers</returns>
    public List<Customer> GetCustomersTimeOnlyRandom(int count)
    {
        var identifier = 1;
        Randomizer.Seed = new Random(338);

        var faker = new Faker<Customer>()
            .CustomInstantiator(f => new Customer { Id = identifier++ })
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(c => c.LastName, f => f.Name.LastName())
            .RuleFor(c => c.Time, f =>
                f.PickRandom(new TimeOnly(9, 0),
                    new TimeOnly(12, 0),
                    new TimeOnly(15, 0)))
            .RuleFor(c => c.Birthdate, f =>
                f.Date.BetweenDateOnly(
                    new DateOnly(1950, 1, 1),
                    new DateOnly(2010, 1, 1)));

        return faker.Generate(count);

    }

    /// <summary>
    /// Generate a list of customers using RecentDateOnly and RecentTimeOnly.
    /// </summary>
    /// <param name="count">How many customers to generate</param>
    /// <returns>Customers</returns>
    public List<Customer> GetCustomersDateOnlyTimeOnlyRecent(int count)
    {

        var identifier = 1;
        Randomizer.Seed = new Random(338);

        var faker = new Faker<Customer>()
            .CustomInstantiator(f => new Customer { Id = identifier++ })
            .RuleFor(c => c.FirstName, f => f.Name.FirstName())
            .RuleFor(c => c.LastName, f => f.Name.LastName())
            .RuleFor(c => c.Time, f => f.Date.RecentTimeOnly())
            .RuleFor(c => c.Birthdate, f => f.Date.RecentDateOnly());

        return faker.Generate(count);

    }

    /// <summary>
    /// Generate a list of Customers using SoonDateOnly and SoonTimeOnly.
    /// </summary>
    /// <param name="count">How many customers to generate</param>
    /// <returns>Customers</returns>
    public List<Customer> GetCustomersTimeOnlySoon(int count)
    {
        var identifier = 1;
        Randomizer.Seed = new Random(338);

        var faker = new Faker<Customer>()
            .CustomInstantiator(f => new Customer { Id = identifier++ })
            .RuleFor(c => c.FirstName, f => f.Name.FirstName())
            .RuleFor(c => c.LastName, f => f.Name.LastName())
            .RuleFor(c => c.Time, f => f.Date.SoonTimeOnly())
            .RuleFor(c => c.Birthdate, f => f.Date.SoonDateOnly());

        return faker.Generate(count);

    }

    /// <summary>
    /// Generate a list of Customers using SoonDateOnly and BetweenDateOnly.
    /// </summary>
    /// <param name="count">How many customers to generate</param>
    /// <param name="minDate"></param>
    /// <param name="maxDate"></param>
    /// <returns>Customers</returns>
    public List<Customer> GetCustomersTimeOnlySoon(int count, DateOnly minDate, DateOnly maxDate)
    {
        var identifier = 1;
        Randomizer.Seed = new Random(338);

        var faker = new Faker<Customer>()
            .CustomInstantiator(f => new Customer { Id = identifier++ })
            .RuleFor(c => c.FirstName, f => f.Name.FirstName())
            .RuleFor(c => c.LastName, f => f.Name.LastName())
            .RuleFor(c => c.Time, f => f.Date.SoonTimeOnly())
            .RuleFor(c => c.Birthdate, f =>
                f.Date.BetweenDateOnly(minDate, maxDate));

        return faker.Generate(count);

    }



    /// <summary>
    /// Generate a list of Customers using RecentTimeOnly and BetweenDateOnly.
    /// </summary>
    /// <param name="count">How many customers to generate</param>
    /// <returns>Customers</returns>
    public List<Customer> GetCustomersBetweenTimeOnlyAndDateOnly(int count)
    {
        var identifier = 1;
        Randomizer.Seed = new Random(338);

        var faker = new Faker<Customer>()
            .CustomInstantiator(f => new Customer { Id = identifier++ })
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(c => c.LastName, f => f.Name.LastName())
            .RuleFor(c => c.Time, f => f.Date.BetweenTimeOnly(
                new TimeOnly(8,0,0),
                new TimeOnly(14,0,0)))
            .RuleFor(c => c.Birthdate, f =>
                f.Date.BetweenDateOnly(
                    new DateOnly(1950, 1, 1),
                    new DateOnly(2010, 1, 1)));

        return faker.Generate(count);

    }
}