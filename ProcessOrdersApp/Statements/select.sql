--- localdb Northwind2024

DECLARE @StartDate AS DATE= '07-08-2014';
DECLARE @EndDate AS DATE= '07/15/2015';
SELECT O.OrderID,
       O.OrderDate,
       O.RequiredDate,
       O.ShippedDate,
       O.ShipAddress,
       O.ShipCity,
       O.ShipPostalCode,
       O.ShipCountry,
       C.CompanyName
FROM Orders AS O INNER JOIN Customers AS C ON O.CustomerIdentifier = C.CustomerIdentifier
WHERE O.OrderDate BETWEEN @StartDate AND @EndDate