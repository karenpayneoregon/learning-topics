#nullable disable

using System.ComponentModel.DataAnnotations;

namespace BogusLibrary1.Models;

/// <summary>
/// Represents a customer with personal details and formatted date and time properties.
/// </summary>
/// <remarks>
/// DateOnly and TimeOnly properties do not format properly using DisplayAttribute thus their formatted versions are provided as properties.
/// </remarks>
public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [Display(Name = "Birthdate")]
    public string BirthdateFormatted => Birthdate.ToString("MM/dd/yyyy");

    public DateOnly Birthdate { get; set; }

    [Display(Name = "Time")]
    public string TimeFormatted => Time.ToString("hh:mm tt");

    public TimeOnly Time { get; set; }
}
