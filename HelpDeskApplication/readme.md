# About

An example for displaying property values from appsettings.json in _Layout.cshtml

> **Note**
> Unlike the other projects under the Configuration folder, this project does not use the in-memory provider; all configuration values are read from the `appsettings.json` file.


## Model

```csharp
public class HelpDesk
{
    public string Phone { get; set; }
    public string Email { get; set; }
}
```

## Program.cs

```csharp
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();

        builder.Services.Configure<HelpDesk>(
            builder.Configuration.GetSection(nameof(HelpDesk)));
```

## _Layout.cshtml

Required using statements

- IConfiguration Configuration for reading `appsettings.json`
- ILogger&lt;dynamic> Logger for `Serilog`

```html
@inject IConfiguration Configuration
@inject ILogger<dynamic> Logger
```

**Footer**

```html
<footer class="border-top footer text-muted">

    <div class="container">

        @{
            Logger.LogInformation("Reading help desk value...");

            var helpDesk = Configuration.GetSection(nameof(HelpDesk)).Get<HelpDesk>();
            var email = helpDesk!.Email;
            var phone = helpDesk.Phone;
        }

        <span class="text-success fw-bold">Help Desk:</span> <strong>Phone</strong> @phone
        <div class="vr opacity-100"></div> <strong>Email</strong> @email

    </div>
</footer>
```

## Index.cshtml

Has the basics for `IOptionsSnapshot` to be injected into the page for the `HelpDesk` model.


```csharp
public class IndexModel : PageModel
{
    private readonly IOptionsSnapshot<HelpDesk> _helpdeskSnapshot;

    public IndexModel(IOptionsSnapshot<HelpDesk> helpdeskSnapshot)
    {
        _helpdeskSnapshot = helpdeskSnapshot;
    }
    public void OnGet()
    {
    }
}
```
