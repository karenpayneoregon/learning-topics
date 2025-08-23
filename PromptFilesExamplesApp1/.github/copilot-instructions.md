## Purpose

This file gives short, targeted guidance for AI coding agents working on PromptFilesExamplesApp1 so they can be immediately productive. It documents the app's structure, key integration points, local conventions, and exact commands to build/run.

## Quick start (build & run)

Use PowerShell in the repo root:

```powershell
dotnet build
dotnet run --project .
```

Notes: `appsettings.json` is copied to output (`<None Update="appsettings.json" CopyToOutputDirectory=PreserveNewest/>`). There is no solution file in the repo root.

## Big picture

- Type: small .NET 9 console app (TargetFramework `net9.0`). See `PromptFilesExamplesApp1.csproj`.
- Entry points: top-level `Program.cs` calls `DataOperations.GetSettings()` and `SpectreConsoleHelpers.ExitPrompt()`; the real app initialization happens in `Classes/Program.cs` via a `ModuleInitializer`.
- Initialization: `Classes/Program.cs` sets console title, window position, invokes `SetupLogging.Development()` and `Setup()` which calls `ConfigureServices()` and uses `SetupServices` to read configuration (connection strings, entity settings).
- UI: lightweight Spectre.Console helpers live in `Classes/SpectreConsoleHelpers.cs` (helpers include markup escaping and an exit prompt).
- Logging: configured in `Classes/SetupLogging.cs` — writes logs to `<AppBase>/LogFiles/YYYY-MM-DD/Log.txt` using Serilog.

## Project-specific conventions

- File layout: `Classes/` contains application wiring and helpers; `Models/` contains partial model classes (see `Person.cs` + `Person.Notification.cs`, `Person.Sets.cs` generated nesting). The project uses file nesting (`.filenesting.json`) and the generated `obj/FileNesting.DependentUpon.generated.props`.
- Partial models: models are implemented as `partial` classes so features are split into multiple files (e.g. `Models/Person.cs` is the root; `Person.Notification.cs` and `Person.Sets.cs` are dependent files).
- Configuration access: the app relies on singletons from external configuration libraries (from `ConsoleConfigurationLibrary/ConfigurationLibrary`). Common access patterns:

  - `AppConnections.Instance.MainConnection` (connection strings)
  - `EntitySettings.Instance.CreateNew` (entity-related flags)

  Example usage: `Classes/DataOperations.cs` prints these values — follow that pattern to read settings.

## Integration points & dependencies

- Key NuGet packages (see `PromptFilesExamplesApp1.csproj`): `ConfigurationLibrary`, `ConsoleConfigurationLibrary`, `ConsoleHelperLibrary`, `Serilog`, `Spectre.Console`.
- `ConfigureServices()` is referenced from `Classes/Program.cs` (static import from `ConsoleConfigurationLibrary.Classes.ApplicationConfiguration`). When adding services, prefer registering via that method or the library's extension points.
- Database code is intentionally commented out in the csproj (EF Core / Dapper references are present but disabled). If you add DB code, enable the appropriate packages and follow the project's `SetupServices` pattern for connection discovery.

## What to change and where (common tasks)

- Add a new background service: update `ConfigureServices()` (library) or create a new registration consumed by `SetupServices` in `Classes/Program.cs`.
- Read configuration values: use the `AppConnections.Instance` / `EntitySettings.Instance` singletons — examples are in `Classes/DataOperations.cs` and `Classes/Program.cs`.
- Modify logging: `Classes/SetupLogging.cs` centralizes Serilog config. Keep file path location pattern to preserve existing log folder structure.

## Examples & quick references

- Initialization flow: `Classes/Program.cs` -> `ConfigureServices()` -> `SetupServices.GetConnectionStrings()` / `GetEntitySettings()`
- Print settings sample: `Classes/DataOperations.cs` (two Console.WriteLine calls showing `AppConnections.Instance` and `EntitySettings.Instance`).

## Constraints for AI agents

- Prefer small, isolated changes; keep public APIs stable.
- Don't add heavy new infra (new top-level projects/solutions) without a user ask — this repo is intentionally minimal.
- Verify behavior locally by running `dotnet build` and `dotnet run` before opening a PR. There are no unit tests in the repo.

## Files to inspect first

`Classes/Program.cs`, `Classes/SetupLogging.cs`, `Classes/SpectreConsoleHelpers.cs`, `Classes/DataOperations.cs`, `Models/Person.cs`, `PromptFilesExamplesApp1.csproj`, `appsettings.json`, `readme.md`.

---
If anything here is unclear or you'd like the agent to include extra examples (service registration, adding a command, or enabling DB packages), tell me which area to expand and I'll iterate.
