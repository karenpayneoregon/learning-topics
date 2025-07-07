# About

Prompt for IEnumerable Visualizer  by project

## FieldKeywordSample

- Where not is Customer
- Where is Customer
- Get only Customers where Address.State == "OR"
- Where is Customer Address count is greater than 1
- Where BirthDate.Year < 1975
- Where BirthDate.Year == 1978
- Where BirthDate,Year >= 1978 &&  BirthDate,Year  >= 1986
- people.Where( x => x.BirthDate.Year.Between(1978,1986)

## WineConsoleApp

- get all records were Models.WineType.White
- get all records where WineId is less than 12
- order by Name 
- order by Name and were Models.WineType.White
- GroupBy on Models.WineType and in the Select include .ToList

