using JsonConvertersSampleApp.Models;

namespace JsonConvertersSampleApp.Interfaces;
public interface IHuman
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    public Gender Gender { get; set; }
}