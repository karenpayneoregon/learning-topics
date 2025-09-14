# Instructions

1. Create BirthDaysDatabase under `(localdb)\MSSQLLocalDB`
1. Run the `populate.sql` script to create the `BirthDays` table and insert sample data.

## Connection String

Is in `appsettings.json` is the database is to be created on a different server or with different credentials.

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "MainConnection": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BirthDaysDatabase;Integrated Security=True;Encrypt=False"
  },
  "EntityConfiguration": {
    "CreateNew": true
  }
}
```