using PartialSamples1.Models;

namespace PartialSamples1.Interfaces;

public interface IPerson
{
    int Id { get; set; }
    string? FirstName { get; set; }
    string? LastName { get; set; }
    Gender? Gender { get; set; }
}