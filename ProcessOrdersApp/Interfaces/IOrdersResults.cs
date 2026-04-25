using Serilog;

namespace ProcessOrdersApp.Interfaces;

public interface IOrdersResults
{
    bool Process { get; set; }
    int OrderID { get; set; }
    DateOnly OrderDate { get; set; }
    DateOnly RequiredDate { get; set; }
    DateOnly ShippedDate { get; set; }
    string ShipAddress { get; set; }
    string? ShipCity { get; set; }
    string? ShipPostalCode { get; set; }
    string? ShipCountry { get; set; }
    string CompanyName { get; set; }
}