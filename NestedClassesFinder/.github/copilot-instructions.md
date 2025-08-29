Repository: NestedClassesFinder — Copilot instructions for coding agents

Purpose
- Help AI agents be productive immediately: explain the small console-tool architecture, typical developer workflows, and repo-specific patterns to preserve.

Quick facts
- Target: .NET 9 console app (see `NestedClassesFinder.csproj`).
- Entry point: `Program.cs` (top-level partial program split across `Program.cs` and `Classes/Program.cs`).
- Primary purpose: find C# partial classes whose filenames don't match the declared class and optionally emit a `.filenesting.json` (see `NestedHelper.Find` and `FileNestingWriter.CreateFileNestingJson`).

What to read first (order matters)
- `Program.cs` — high-level run path and sample invocation (writes `output.txt`).
- `Classes/NestedHelper.cs` — core algorithm: traverses directories, skips `bin`/`obj`/`.git`/`.vs`, regex for `partial class` discovery, and ignore-suffix rules.
- `Classes/Standard/FileNestingWriter.cs` — transforms mismatches into `.filenesting.json` structure suitable for dependentFileProviders.
- `Classes/SpectreConsoleHelpers.cs` — console helpers and Spectre.Console markup conventions used in UI output.
- `NestedClassesFinder.csproj` — dependencies (Serilog, Spectre.Console, ConsoleConfigurationLibrary) and copy rules for `appsettings.json`.

Architecture & patterns (concise)
- Single-process console tool with a module initializer in `Classes/Program.cs` that configures console title, window placement, and logging (calls `SetupLogging.Development()` from `Classes/Standard/SetupLogging.cs` via package). Keep module initializer side-effects minimal.
- File discovery uses an explicit stack-based DFS (`EnumerateCsFiles`) and excludes typical build directories. Avoid modifying file traversal semantics unless updating both `EnumerateCsFiles` and unit tests (none present).
- Partial-class regex: change carefully — it's compiled and multiline; tests should validate edge cases like `partial record class` and nested/embedded code regions.

Developer workflows & common commands
- Build: dotnet build (project uses SDK-style csproj targeting net9.0). For PowerShell: `dotnet build`.
- Run locally: `dotnet run --project NestedClassesFinder.csproj` or use the built exe under `bin/Debug/net9.0/NestedClassesFinder.exe`.
- Outputs: `output.txt` is written in working dir; `.filenesting.json` is written to the current dir when invoked via `FileNestingWriter.CreateFileNestingJson` (default path argument).
- Config: `appsettings.json` is copied to output (see csproj). Tests: none in repo — add small unit tests targeting `NestedHelper.EnumerateCsFiles` and `Find` for regressions.

Code conventions & repo-specific rules
- Skip patterns: `NestedHelper` ignores files with base names ending with `.Designer`, `.g`, `.g.i`, `.razor`, `.AssemblyInfo`, `.GlobalUsings`. When adding new ignore rules, update `ignoreNameSuffixes` in `NestedHelper.cs` accordingly.
- FileName matching: comparison is ordinal (case-sensitive) for class vs filename (`baseName.Equals(className, StringComparison.Ordinal)`). If you change this, be deliberate about case-sensitivity cross-platform behavior.
- Program startup: module initializer in `Classes/Program.cs` performs UI/logging setup; keep heavy IO out of module initializers to avoid surprising test harness behaviour.

Integration & dependencies
- Uses private helper packages: `ConsoleConfigurationLibrary` and `ConsoleHelperLibrary` (look in `bin/Debug/...` assemblies for behavior). Treat these as black-box: read their public types used here (`ApplicationConfiguration`, `WindowUtility`, `SetupLogging`).
- Logging: Serilog configured via `SetupLogging.Development()` (in `SetupLogging.cs` under `Classes/Standard` or an external package). Use the same pattern for new logging calls.

Examples (copy-paste ready)
- To find mismatches for a path and write `.filenesting.json` into current dir: call `FileNestingWriter.CreateFileNestingJson("C:\\MyRepo")`.
- To enumerate files without touching disk writes in tests: call `NestedHelper` helpers indirectly by extracting `EnumerateCsFiles` (internal to file) — prefer adding a small internal-friendly wrapper or using a test directory on disk.

When editing, check these files after any change
- `Program.cs`, `Classes/NestedHelper.cs`, `Classes/Standard/FileNestingWriter.cs`, `Classes/SpectreConsoleHelpers.cs`, and `NestedClassesFinder.csproj`.

What not to change without explicit reason
- Directory-skip logic and ignore-suffixes — changing these alters tool output significantly.
- The partial-class regex flags and capture group numbers — small changes can break detection.
- Module initializer side-effects (console title, window positioning, SetupLogging) — they run at assembly load.

If you need more context
- There are no unit tests or CI files. If you're adding tests, follow the project's minimal footprint: create a test project targeting net9.0 and keep tests focused on `NestedHelper` logic.

Questions for the maintainer
- Do you want case-sensitive file-vs-class matching preserved on Windows? (Currently uses ordinal case-sensitive.)
- Preferred place for unit tests (new `tests/` folder or sibling project)?

---
If anything here is unclear or you want more examples (unit test templates, CI steps), tell me which section to expand.
