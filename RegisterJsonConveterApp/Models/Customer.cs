using System.Text.Json.Serialization;
using RegisterJsonConveterApp.JsonConverters;

namespace RegisterJsonConveterApp.Models;

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