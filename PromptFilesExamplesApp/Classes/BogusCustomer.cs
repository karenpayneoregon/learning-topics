using Bogus;
using Bogus.DataSets;
using PromptFilesExamplesApp.Models;

namespace PromptFilesExamplesApp.Classes;

public class BogusCustomer
{
    /// <summary>
    /// Generates a list of fake customer data.
    /// </summary>
    /// <param name="count">
    /// The number of customers to generate. Defaults to 10 if not specified.
    /// </param>
    /// <returns>
    /// A list of <see cref="Customer"/> objects populated with random data.
    /// </returns>
    /// <remarks>
    /// * This method was created by GitHub Copilot using the following prompt: BogusCustomers.prompt.md
    /// * FirstName was not gender aware which comes from training so Karen altered the code
    /// </remarks>
    public static List<Customer> GenerateCustomers(int count = 10)
    {
        Bogus.Randomizer.Seed = new Random(338);
        int id = 1;
        var faker = new Faker<Customer>()
            .RuleFor(c => c.Id, f => id++)
            .RuleFor(c => c.Gender, f => f.PickRandom<Gender>())
            .RuleFor(c => c.FirstName, (f, c) => f.Name.FirstName((Name.Gender?)c.Gender))
            .RuleFor(c => c.LastName, f => f.Name.LastName())
            .RuleFor(c => c.BirthDay, f => DateOnly.FromDateTime(f.Date.Past(40, DateTime.Today.AddYears(-18))))
            .RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.FirstName, c.LastName));
        return faker.Generate(count);
    }
}
