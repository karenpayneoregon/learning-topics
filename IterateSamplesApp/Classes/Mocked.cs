namespace IterateSamplesApp.Classes;

public static class Mocked
{
    public static List<Category> GetCategories() =>
    [
        new Category { Id = 1, Name = "Books" },
        new Category { Id = 2, Name = "Electronics" },
        new Category { Id = 3, Name = "Clothing" },
        new Category { Id = 4, Name = "Groceries" },
        new Category { Id = 5, Name = "Toys" },
        new Category { Id = -1, Name = "Select" }
    ];
}