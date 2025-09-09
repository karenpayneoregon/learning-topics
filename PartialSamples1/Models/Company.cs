#pragma warning disable CS8618
namespace PartialSamples1.Models;

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
    public override string ToString() => Name;
}