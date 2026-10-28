using System.Text.Json.Serialization;
using JsonConvertersSampleApp.Interfaces;
using JsonConvertersSampleLibrary.Converters;
using JsonConvertersSampleLibrary.Extensions;

namespace JsonConvertersSampleApp.Models;

/// <summary>
/// Represents a taxpayer with properties for Social Security Number (SSN), Personal Identification Number (PIN),
/// start date, and Employer Identification Number (EIN). Inherits from <see cref="Person"/> and implements <see cref="ITaxpayer"/>.
/// </summary>
public  class Taxpayer : Person, ITaxpayer
{
    /// <summary>
    /// Gets or sets the Social Security Number (SSN) of the taxpayer.
    /// The SSN is masked during JSON serialization using the <see cref="SocialSecurityMaskConverter"/>.
    /// </summary>
    [JsonConverter(typeof(SocialSecurityMaskConverter))]
    public string SSN { get; set; }
    
    public string PIN { get; set; }
    public DateOnly StartDate { get; set; }
    public string EmployerIdentificationNumber { get; set; }
    public override string ToString() => SSN.MaskSsn();
}