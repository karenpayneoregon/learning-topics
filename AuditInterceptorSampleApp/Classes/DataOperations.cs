using AuditInterceptorSampleApp.Data;
using Microsoft.EntityFrameworkCore;

namespace AuditInterceptorSampleApp.Classes;
/// <summary>
/// Performs data operations.
/// </summary>
internal class DataOperations
{
    public static async Task UpdateRecords()
    {
        await using var context = new BookContext();

        int identifier = 1;
        var book = await context.Books.FirstOrDefaultAsync(x => x.Id == identifier);
        book!.Title = "C# in Depth - changed";
        book.Price += 10;


        identifier = 4;
        book = await context.Books.FirstOrDefaultAsync(x => x.Id == identifier);
        book!.Title = "Entity Framework Core in Action - updated";
        book.Price -= 10;

        await context.SaveChangesAsync();
    }
}
