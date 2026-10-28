using JsonConvertersSampleApp.Interfaces;
using System.Text.Json.Serialization;
#nullable disable
namespace JsonConvertersSampleApp.Models;

public class Person : IHuman
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Title { get; set; }
    public DateOnly BirthDate { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Gender Gender { get; set; }
}
