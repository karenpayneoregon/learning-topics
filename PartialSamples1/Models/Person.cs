#nullable disable

using PartialSamples1.Interfaces;
using System.ComponentModel;

namespace PartialSamples1.Models;
public partial class Person : IPerson, INotifyPropertyChanged
{
    public partial int Id { get; set; }
    public partial string FirstName { get; set; }
    public partial string LastName { get; set; }
    public partial Gender? Gender { get; set; }
}
