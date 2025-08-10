# Copilot Coding Agent Instructions for WineConsoleApp

## Project Overview
- This is a .NET console application demonstrating advanced enum usage with Entity Framework Core 8.
- The app auto-creates and seeds the database on first run if it does not exist.
- The main data model is `Wine`, with a key enum property `WineType` (see `Models/WineType.cs`).
- Enum-to-database mapping uses EF Core's `HasConversion<int>()` pattern (see `Data/WineContext.cs`).
- Grouping and filtering by enum is common (see `WineGroupItem` and LINQ examples in `readme.md`).

## Key Files & Structure
- `Program.cs` and `Classes/Startup.cs`: App entry and setup.
- `Data/WineContext.cs`: EF Core context, DB creation, and enum conversion logic.
- `Models/WineType.cs`, `Models/WineTypes.cs`, `Models/WineType.tt`: Enum, backing table, and T4 template for code generation.
- `Models/WineGroupItem.cs`: Used for grouping wines by type.
- `Scripts/Populate.sql`: SQL for seeding data (used by app on first run).

## Code Generation & T4
- `Models/WineType.tt` generates the `WineType` enum from the `WineTypes` table.
- To update the enum after DB changes, right-click the `.tt` file and select "Run Custom Tool" (or set `Custom Tool` property to `TextTemplatingFileGenerator`).
- See `Models/readme.md` for T4 and Visual Studio/VS Code tips.

## Patterns & Conventions
- Enum values are stored as integers in the DB, not strings.
- Always use `HasConversion<int>()` for enum properties in EF models.
- Use `WineGroupItem` for grouping results by enum type.
- When adding new wine types, update the DB and re-run the T4 template to sync the enum.

## Developer Workflows
- **Build/Run:** Use standard `dotnet build` and `dotnet run` commands.
- **Database:** No manual DB setup needed; app creates and seeds DB if missing.
- **Code Generation:** Update `WineTypes` table, then re-run `WineType.tt`.
- **Debugging:** Main logic is in `Program.cs` and `Classes/WineOperations.cs`.

## External Dependencies
- Entity Framework Core 8 (see `WineConsoleApp.csproj` for packages).
- T4 code generation (built-in to Visual Studio, or use VS Code extension for editing).

## Examples
- Filtering: `context.Wines.Where(wine => wine.WineType == WineType.Rose)`
- Grouping: `context.Wines.GroupBy(w => w.WineType).Select(w => new WineGroupItem(w.Key, w.ToList()))`

## References
- See `readme.md` (root and `Models/`) for more details and code samples.
- For T4, see Microsoft docs: https://learn.microsoft.com/en-us/visualstudio/modeling/design-time-code-generation-by-using-t4-text-templates?view=vs-2022&tabs=csharp
