#nullable disable
#nullable disable
using System.ComponentModel.DataAnnotations;

namespace SpectreConsoleMenuApp.Models;

public partial class UserLogin
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string EmailAddress { get; set; }

    [Required]
    [StringLength(5)]
    public string Pin { get; set; }

    public override string ToString() => $"{Id,-4}{EmailAddress}";

}