#nullable disable

using PartialSamples1.Interfaces;

namespace PartialSamples1.Models;
public partial class Person : IPerson
{
    public partial int Id { get; set; }
    public partial string FirstName { get; set; }
    public partial string LastName { get; set; }
    public partial Gender? Gender { get; set; }
}
