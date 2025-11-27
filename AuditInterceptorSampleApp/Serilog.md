# About

The section `SerilogSection` in the `appsettings.json` file is used to configure Serilog logging for the application. It specifies the filename where the log files will be stored.

The class `FileHelper` gets the log file path from this section to get the last log file path and name

```json
{
  "ConnectionStrings": {
    "MainConnection": "Server=(localdb)\\mssqllocaldb;Database=EF.BookCatalog1;Trusted_Connection=True"
  },
  "EntityConfiguration": {
    "CreateNew": true
  },
  "SerilogSection": {
    "FileName": "LogFiles/EF-Log.txt"
  }
}
```

## Note

Requu=ires the following in the project file:

```xml
<ItemGroup>
   <Using Include="ConsoleConfigurationLibrary.Classes.Configuration" Alias="Config" />
</ItemGroup>
```