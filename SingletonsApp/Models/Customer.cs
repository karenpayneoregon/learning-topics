#nullable disable

namespace SingletonsApp.Models;

public class Customer 
{
    public int Id { get; set; }
    public  string FirstName { get; set; }
    public  string LastName { get; set; }
    public  Gender Gender { get; set; }
    public  DateOnly DateOfBirth { get; set; }
}

public enum Gender
{
    Male,
    Female,
    Other
}