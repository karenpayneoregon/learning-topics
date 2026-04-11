SELECT O.OrderID,
       O.OrderDate,
       O.RequiredDate,
       O.ShippedDate,
       REPLACE(O.ShipAddress, ',', ' ') AS ShipAddress,
       O.ShipCity,
       O.ShipPostalCode,
       O.ShipCountry,
       C.CompanyName
FROM Orders AS O INNER JOIN Customers AS C ON O.CustomerIdentifier = C.CustomerIdentifier;
