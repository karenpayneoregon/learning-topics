

namespace DeconstructSamplesApp.Models;

public class Person
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateOnly DateOfBirth { get; set; }
    public required Gender Gender { get; set; }
}

public enum Gender
{
    Female,
    Male
}