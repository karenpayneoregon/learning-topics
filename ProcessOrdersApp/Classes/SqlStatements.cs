namespace ProcessOrdersApp.Classes;
internal class SqlStatements
{
    /// <summary>
    /// SQL query to retrieve orders placed between two specified dates.
    /// </summary>
    /// <remarks>
    /// This query selects order details such as order dates, shipping information, 
    /// and customer company names. It uses parameters <c>@StartDate</c> and <c>@EndDate</c> 
    /// to filter orders within the specified date range.
    /// </remarks>
    public static string GetOrdersBetweenDates = """
        SELECT O.OrderID,
               O.OrderDate,
               O.RequiredDate,
               O.ShippedDate,
               REPLACE(O.ShipAddress, ',', ' ') AS ShipAddress,
               O.ShipCity,
               O.ShipPostalCode,
               O.ShipCountry,
               C.CompanyName
        FROM Orders AS O
        INNER JOIN Customers AS C
            ON O.CustomerIdentifier = C.CustomerIdentifier
        ---WHERE O.OrderDate BETWEEN @StartDate AND @EndDate;
        """;
}
