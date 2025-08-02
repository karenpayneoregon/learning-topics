USE [CustomerDatabase1]
GO

SELECT      C.Identifier,
            C.CompanyName,
            CT.ContactType,
            C.ContactFirstName,
            C.ContactLastName,
            G.GenderType,
            C.ContactTypeIdentifier,
            C.GenderIdentifier
  FROM      dbo.Customer AS C
 INNER JOIN dbo.ContactTypes AS CT
    ON C.ContactTypeIdentifier = CT.Identifier
 INNER JOIN dbo.Genders AS G
    ON C.GenderIdentifier      = G.id
 ORDER BY C.CompanyName;