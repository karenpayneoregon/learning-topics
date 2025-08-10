# Copilot Instructions for HelpDeskApplication

## Project Overview
- **Type:** ASP.NET Core Razor Pages application
- **Purpose:** Demonstrates reading configuration values from `appsettings.json` and displaying them in the layout/footer.
- **Key Models:**
  - `HelpDesk` (Models/HelpDesk.cs): Holds `Phone` and `Email` for support contact.
  - `DatabaseSettings` (Models/DatabaseSettings.cs): Holds `DatabasePassword` (populated from secrets or config).

## Architecture & Data Flow
- **Configuration:**
  - All configuration values are read from `appsettings.json` (and `appsettings.Development.json` for dev overrides).
  - Additional secrets (e.g., `DatabasePassword`) are loaded from the `Secrets/` directory using the KeyPerFile provider (see `Program.cs`).
- **Dependency Injection:**
  - `HelpDesk` is injected via `IOptionsSnapshot<HelpDesk>` (see `Pages/Index.cshtml.cs`).
  - `DatabaseSettings` is injected via `IOptions<DatabaseSettings>`.
- **Logging:**
  - Uses Serilog for logging (see `Program.cs`).
  - Logger is injected into Razor views as `ILogger<dynamic>`.
- **UI Integration:**
  - The footer in `Pages/Shared/_Layout.cshtml` reads and displays Help Desk contact info from config.
  - Example:
    ```cshtml
    @inject IConfiguration Configuration
    @inject ILogger<dynamic> Logger
    ...
    var helpDesk = Configuration.GetSection(nameof(HelpDesk)).Get<HelpDesk>();
    ```

## Developer Workflows
- **Build:**
  - Standard .NET build: `dotnet build` from the project root.
- **Run:**
  - `dotnet run` from the project root.
- **Debug:**
  - Launch via Visual Studio or `dotnet run` for local debugging.
- **Secrets:**
  - Place secret files (e.g., `DatabasePassword`) in the `Secrets/` directory. Each file's name is the config key, and its contents are the value.

## Project-Specific Conventions
- **No in-memory config provider**: All config comes from files or secrets, not code.
- **Razor Pages only**: No MVC controllers.
- **Serilog**: All logging is via Serilog, configured in `Program.cs`.
- **KeyPerFile secrets**: Used for sensitive values, not user secrets or environment variables.

## Key Files & Directories
- `Program.cs`: App startup, DI, config, logging setup
- `Models/`: Data models for config binding
- `Pages/Shared/_Layout.cshtml`: Footer logic for displaying Help Desk info
- `Pages/Index.cshtml.cs`: Example of DI for config models
- `Secrets/`: Place for key-per-file secrets (not checked in)

## Patterns & Examples
- **Inject config in Razor:**
  ```cshtml
  @inject IConfiguration Configuration
  var helpDesk = Configuration.GetSection("HelpDesk").Get<HelpDesk>();
  ```
- **Inject config in PageModel:**
  ```csharp
  public IndexModel(IOptionsSnapshot<HelpDesk> helpdeskSnapshot, IOptions<DatabaseSettings> options)
  ```

---

For more, see `readme.md` and referenced files. Update this file if project structure or conventions change.
