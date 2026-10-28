using System.Text.Json.Serialization;
using JsonConvertersSampleLibrary.Converters;

namespace JsonConvertersSampleApp.Models;

/// <summary>
/// Represents a customer with personal details and sensitive information such as credit card number and PIN.
/// Inherits from the <see cref="Person"/> class.
/// </summary>
public class Customer : Person
{
    /// <summary>
    /// Gets or sets the credit card number of the customer.
    /// The credit card number is masked when serialized to JSON using the <see cref="CreditCardMaskConverter"/>.
    /// </summary>
    [JsonConverter(typeof(CreditCardMaskConverter))]
    public string CreditCardNumber { get; set; }
    public string PIN { get; set; }
}