using Bogus;
using static Bogus.Randomizer;
using WithStatementApp.Models;

namespace WithStatementApp.Classes;

public class MockedData
{
    public static (List<Product> products, List<Category> categories) GetProductsAndCategories()
    {

        Seed = new Random(334);

        var categoryFaker = new Faker<Category>()
            .RuleFor(c => c.Id, f => f.IndexFaker + 1)
            .RuleFor(c => c.Name, f => f.Commerce.Categories(1)[0]);

        var categories = categoryFaker.Generate(3);

        return ([], categories);
    }
}
