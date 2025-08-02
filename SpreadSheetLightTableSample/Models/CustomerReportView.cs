namespace SpreadSheetLightTableSample.Models;

/// <summary>
/// Represents a view model for customer reports, containing customer-related information 
/// such as identifier, contact details, gender type, and contact type.
/// </summary>
public class CustomerReportView
{
    public int Identifier { get; set; }
    public string ContactFirstName { get; set; }
    public string ContactLastName { get; set; }
    public string GenderType { get; set; }
    public string ContactType { get; set; }
}
