using System.Text.Json.Serialization;
using UpperCaseFirstCharConverterApp.JsonConverters;

namespace UpperCaseFirstCharConverterApp.Models;

public class Customer
{
    public int Id { get; set; }
    [JsonConverter(typeof(UpperCaseFirstCharConverter))]
    public required string FirstName { get; set; }
    [JsonConverter(typeof(UpperCaseFirstCharConverter))]    
    public required string LastName { get; set; }
    public required string State { get; set; }
    public DateOnly BirthDate { get; set; }
}