using AuditInterceptorSampleApp.Data;
using AuditInterceptorSampleApp.Models;

namespace AuditInterceptorSampleApp.Classes.Configurations;

internal class SetupDatabase
{
    /// <summary>
    /// Creates the database if it does not already exist.
    /// This method does not populate the <see cref="Category"/> table.
    /// </summary>
    /// <returns>
    /// A tuple containing a boolean indicating success or failure, and an <see cref="Exception"/> if an error occurs.
    /// </returns>
    public static async Task<(bool success, Exception exception)> InitialCreateDatabase()
    {
        try
        {
            await using var context = new BookContext();

            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
            
            return (true, null)!;
            
        }
        catch (Exception localException)
        {

            return (false, localException);
        }
    }

    /// <summary>
    /// Creates and populates the database with initial data.
    /// This method adds predefined rows to the <see cref="Category"/> and <see cref="Book"/> tables.
    /// </summary>
    /// <returns>
    /// A tuple containing a boolean indicating success or failure, and an <see cref="Exception"/> if an error occurs.
    /// </returns>
    public static async Task<(bool success, Exception exception)> CreateDatabase()
    {
        try
        {
            await using var context = new BookContext();
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();


            context.Categories.Add(new Category() {  Description = "Learn C#"});
            context.Categories.Add(new Category() {  Description = "EF Core 7"});

            await context.SaveChangesAsync();

            context.Books.Add(new Book()
            {
                Price = 44.99m,
                Title = "C# in Depth",
                CategoryId = 1
            });

            context.Books.Add(new Book()
            {
                Price = 31.99m,
                Title = "C# 11 and .NET 7 – Modern Cross-Platform Development Fundamentals: Start building websites and services with ASP.NET Core 7, Blazor, and EF Core 7",
                CategoryId = 1
            });

            context.Books.Add(new Book()
            {
                Price = 43.99m,
                Title = "Apps and Services with .NET 7: Build practical projects with Blazor, .NET MAUI, gRPC, GraphQL, and other enterprise technologies",
                CategoryId = 1
            });

            context.Books.Add(new Book()
            {
                Price = 55.99m,
                Title = "Entity Framework Core in Action",
                CategoryId = 2
            });


            context.Books.Add(new Book()
            {
                Price = 45.99m,
                Title = "Practical Entity Framework Core 6 : Database Access for Enterprise Applications",
                CategoryId = 2
            });

            var count = await context.SaveChangesAsync();

            return (true, null)!;
            
        }
        catch (Exception localException)
        {
            return (false, localException);
        }
    }
}