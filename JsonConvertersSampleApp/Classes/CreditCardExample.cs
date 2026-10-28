using System.Diagnostics;
using System.Text.Json;
using JsonConvertersSampleApp.Models;
using JsonConvertersSampleLibrary.Classes;
using JsonConvertersSampleLibrary.Converters;
using JsonConvertersSampleLibrary.Extensions;

namespace JsonConvertersSampleApp.Classes;

internal class CreditCardExample
{
    /// <summary>
    /// Serializes a list of <see cref="Customer"/> objects to a JSON file.
    /// CreditCardNumber is masked using the <see cref="CreditCardMaskConverter"/>.
    /// </summary>
    /// <remarks>
    /// This method creates two sample <see cref="Customer"/> objects, serializes them to JSON format,
    /// and writes the JSON string to a file named "Customers.json".
    /// </remarks>
    public static void ForCustomers()
    {
        var customer1 = new Customer
        {
            Id = 1,
            FirstName = "John",
            LastName = "Jones",
            Title = "Mr.",
            CreditCardNumber = "123456 7890 123456",
            PIN = "5555",
            BirthDate = new DateOnly(1980, 1, 1),
        };

        var customer2 = new Customer
        {
            Id = 2,
            FirstName = "Mary",
            LastName = "Gallagher",
            Title = "Miss.",
            CreditCardNumber = "123 456 789 012 3456",
            PIN = "1234",
            BirthDate = new DateOnly(1980, 1, 1),
        };

        List<Customer> customers = [customer1, customer2];
        var json = JsonSerializer.Serialize(customers, Options);

        File.WriteAllText("Customers.json", json);

    }

    public static void FromExternal()
    {
        CreditCard cc = new CreditCard();
        string text = "6011-1111-1111-1117";
        Debug.WriteLine(text.CreditCardShowLastFourDigits());
    }

    private static JsonSerializerOptions Options =>
        new()
        {
            WriteIndented = true
        };
}
