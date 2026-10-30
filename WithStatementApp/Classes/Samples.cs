using Bogus.Bson;
using WithStatementApp.Models;

namespace WithStatementApp.Classes;

internal class Samples
{
    public static void BaseExample()
    {
        List<Product> products = [];

        Product product = new(1, "Football", 2);
        products.Add(product);
        products.Add(product with { Id = 2, Name = "Golf" });
        
        foreach (var (id, name, categoryId) in products)
        {
            Console.WriteLine($"{id,-3}: {name,-20}{categoryId}");
        }
    }
    public static void ProductsExample()
    {
        // Generates sample products and categories
        var (products, categories) = MockedData.GetProductsAndCategories();

        Dictionary<int, string> dictionary = new()
        {
            [2] = "Golf",
            [3] = "Formula 1 racing",
            [4] = "Soccer"
        };

        Product product = new(1, "Football", 2);
        products.Add(product);

        foreach (var (key, value) in dictionary)
        {
            products.Add(product with { Id = key, Name = value });
        }

        var joined =
            products.Join(
                categories,
                p => p.CategoryId,
                c => c.Id,
                (p, c) => new
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    CategoryName = c.Name
                });

        foreach (var item in joined)
        {
            Console.WriteLine($"{item.ProductId,-3}: {item.ProductName,-20}{item.CategoryName}");
        }

    }
}