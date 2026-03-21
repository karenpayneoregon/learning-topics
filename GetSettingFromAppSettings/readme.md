# About
This project demonstrates how to retrieve settings from the `appsettings.json` file in an ASP.NET Core application. It focuses on `logging settings` and how to access them in Razor Pages.

Also shows [ValidateOnStart()](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.optionsbuilderextensions.validateonstart?view=net-10.0-pp)

```csharp
if (!builder.Configuration.GetSection("Logging").Exists())
{
    throw new InvalidOperationException("Configuration section 'Logging' is missing.");
}

builder.Services
    .AddOptions<LoggingSettings>()
    .Bind(builder.Configuration.GetSection("Logging"))
    .ValidateOnStart();

builder.Services.AddSingleton<IValidateOptions<LoggingSettings>, LoggingSettingsValidation>();
```

---


```json
{
  "ConnectionStrings": {
    "MainConnection": "Data Source=.\\SQLEXPRESS;Initial Catalog=AppsettingsConfigurations;Integrated Security=True;Encrypt=False"
  },
  "EntityConfiguration": {
    "CreateNew": true
  },
  "Logging": {
    "LogLevel": {
      "Microsoft.EntityFrameworkCore.Database.Command": "Information",
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```
---

```csharp
public class LogLevelSettings
{
    public string Default { get; set; } = string.Empty;

    [ConfigurationKeyName("Microsoft.AspNetCore")]
    public string Microsoft_AspNetCore { get; set; } = string.Empty;

    [ConfigurationKeyName("Microsoft.EntityFrameworkCore.Database.Command")]
    public string MicrosoftEntityFrameworkCoreDatabaseCommand { get; set; } = string.Empty;
}
```