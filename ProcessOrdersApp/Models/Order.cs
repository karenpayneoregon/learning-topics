namespace ProcessOrdersApp.Models;

/// <summary>
/// Represents the result of an order with detailed information including dates, shipping details, and company name.
/// </summary>
public class Order(int orderId, DateOnly orderDate, DateOnly requiredDate, DateOnly shippedDate, string shipAddress, string? shipCity, string? shipPostalCode, string? shipCountry, string companyName)
{
    public int OrderID { get; init; } = orderId;
    public DateOnly OrderDate { get; init; } = orderDate;
    public DateOnly RequiredDate { get; init; } = requiredDate;
    public DateOnly ShippedDate { get; init; } = shippedDate;
    public string ShipAddress { get; init; } = shipAddress;
    public string? ShipCity { get; init; } = shipCity;
    public string? ShipPostalCode { get; init; } = shipPostalCode;
    public string? ShipCountry { get; init; } = shipCountry;
    public string CompanyName { get; init; } = companyName;

    public void Deconstruct(out int Id, out DateOnly Ordered, out DateOnly RequiredBy, out DateOnly Shipped, out string Address, out string? City, out string? PostalCode, out string? Country, out string Company)
    {
        Id = OrderID;
        Ordered = this.OrderDate;
        RequiredBy = this.RequiredDate;
        Shipped = this.ShippedDate;
        Address = this.ShipAddress;
        City = this.ShipCity;
        PostalCode = this.ShipPostalCode;
        Country = this.ShipCountry;
        Company = this.CompanyName;
    }
}