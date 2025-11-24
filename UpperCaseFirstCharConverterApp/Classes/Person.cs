using System.Text.Json.Serialization;

namespace UpperCaseFirstCharConverterApp.Classes;

public class Person
{
    public int Id { get; set; }
    [JsonConverter(typeof(UpperCaseFirstCharConverter))]
    public required string FirstName { get; set; }
    [JsonConverter(typeof(UpperCaseFirstCharConverter))]    
    public required string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
}