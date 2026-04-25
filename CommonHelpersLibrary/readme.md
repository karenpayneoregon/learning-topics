# CommonHelpersLib<!-- TOC-->
  - [Contents](#contents)
  - [Overview](#overview)
  - [Packages](#packages)
  - [Getting Started](#getting-started)
  - [API Reference](#api-reference)
    - [BoolExtensions](#boolextensions)
    - [DateTimeExtensions](#datetimeextensions)
    - [ConfigurationHelpers](#configurationhelpers)
    - [FileHelper](#filehelper)
    - [Info](#info)
    - [Models](#models)
  - [Configuration File Expectations](#configuration-file-expectations)
  - [Usage Examples](#usage-examples)
    - [Boolean Formatting](#boolean-formatting)
    - [Weekend Calculation](#weekend-calculation)
    - [Business Days with DateOnly](#business-days-with-dateonly)
    - [Configuration Value Retrieval](#configuration-value-retrieval)
    - [Latest Log File](#latest-log-file)
    - [Caller-Aware Metadata](#caller-aware-metadata)
  - [Notes](#notes)
<!-- TOC -->rary

Reusable .NET helper library with extension methods and utility classes for:

- Boolean display formatting
- Date and business-day calculations
- Configuration validation and retrieval
- Log file discovery
- Assembly metadata and caller diagnostics

Target framework: .NET 9

## Contents

- [Overview](#overview)
- [Packages](#packages)
- [Getting Started](#getting-started)
- [API Reference](#api-reference)
- [Configuration File Expectations](#configuration-file-expectations)
- [Usage Examples](#usage-examples)

## Overview

The library is designed for shared use across multiple solutions where common infrastructure code is needed. It currently includes:

- `BoolExtensions`
- `DateTimeExtensions` (partial class split across files)
- `ConfigurationHelpers`
- `FileHelper`
- `Info`
- `HolidayItem` model

## Packages

The project depends on the following NuGet packages:

- Microsoft.Extensions.Configuration
- Microsoft.Extensions.Configuration.Abstractions
- Microsoft.Extensions.Configuration.Binder
- Microsoft.Extensions.Configuration.EnvironmentVariables
- Microsoft.Extensions.Configuration.Json
- Microsoft.Extensions.FileSystemGlobbing
- DateTimeExtensions

## Getting Started

1. Add a project reference to this library from your consuming project.
2. Ensure the consuming project targets a compatible framework (recommended: .NET 9).
3. Add the required `using` directives in your code:

```csharp
using CommonHelpersLibrary;
using CommonHelpersLibrary.Models;
```

## API Reference

### BoolExtensions

Extension methods for `bool` values.

- `ToYesNo(this bool value)`
	- Returns `"Yes"` when true, otherwise `"No"`.

### DateTimeExtensions

`DateTimeExtensions` is a partial static class containing helpers for `DateTime` and `DateOnly`.

DateTime methods:

- `GetWeekendDates(this DateTime date, DayOfWeek startOfWeek = DayOfWeek.Sunday)`
- `GetWeekendDatesUtc(this DateTime utcDate, DayOfWeek startOfWeek = DayOfWeek.Sunday)`
	- Throws `ArgumentException` if `utcDate.Kind != DateTimeKind.Utc`.
- `GetPreviousWeekendDates(this DateTime date, DayOfWeek startOfWeek = DayOfWeek.Sunday)`
- `GetNextWeekendDates(this DateTime date, DayOfWeek startOfWeek = DayOfWeek.Sunday)`

DateOnly methods (holiday and business-day aware via `DateTimeExtensions.WorkingDays.WorkingDayCultureInfo`):

- `IsHoliday(this DateOnly day)`
- `IsWorkingDay(this DateOnly day)`
- `AddWorkingDays(DateOnly day, int workingDays)`
- `AddBusinessWeekDay(this DateOnly day)`
- `AllYearHolidays(this DateOnly day)`
- `DateTimeFromDateOnly(DateOnly day, int hour, int minute)`

### ConfigurationHelpers

Helpers for checking and retrieving values from app settings.

File-based checks against `appsettings.json`:

- `EntityConfigurationSectionExists()`
- `ConnectionStringsSectionExists()`
- `MainConnectionExists()`
- `PropertyExists(string section, string propertyName)`
- `PropertyExists(params string[] path)`

`IConfiguration`-based helpers:

- `PropertyExists(IConfiguration configuration, string key)`
- `TryGetValue<T>(IConfiguration configuration, string key, out T value)`
- `GetConfiguration()`
	- Loads `appsettings.json`
	- Optionally loads environment variant: `appsettings.{DOTNET_ENVIRONMENT}.json`
	- Adds environment variables

### FileHelper

Log file discovery helpers.

- `GetLogFileName()`
	- Searches for `*.txt` files under:
		- `{AppDomain.CurrentDomain.BaseDirectory}/LogFiles`
	- Returns the newest file by `LastWriteTimeUtc`.
	- Throws `DirectoryNotFoundException` if `LogFiles` directory is missing.

### Info

Assembly metadata utilities based on the calling assembly.

- `GetCopyright()`
- `GetCompany()`
- `GetProduct()`
- `GetVersion()`

Overloads with caller details are also provided:

- `GetCopyright(out CallerDetails caller, ...)`
- `GetCompany(out CallerDetails caller, ...)`
- `GetProduct(out CallerDetails caller, ...)`
- `GetVersion(out CallerDetails caller, ...)`

`CallerDetails` contains:

- Assembly name
- Assembly version
- Target framework
- Type name
- Method name
- Source file path
- Source line number

### Models

`HolidayItem`

- `Name` (required)
- `Date`

## Configuration File Expectations

Some `ConfigurationHelpers` methods read from `appsettings.json` in the current working directory.

Example structure:

```json
{
	"EntityConfiguration": {
		"UseSoftDelete": true
	},
	"ConnectionStrings": {
		"MainConnection": "Server=.;Database=AppDb;Trusted_Connection=True;"
	}
}
```

## Usage Examples

### Boolean Formatting

```csharp
bool enabled = true;
string text = enabled.ToYesNo(); // "Yes"
```

### Weekend Calculation

```csharp
DateTime today = DateTime.Today;
var (saturday, sunday) = today.GetWeekendDates();
```

### Business Days with DateOnly

```csharp
DateOnly start = new DateOnly(2026, 4, 25);
DateOnly due = DateTimeExtensions.AddWorkingDays(start, 10);
bool isWorkingDay = start.IsWorkingDay();
```

### Configuration Value Retrieval

```csharp
using Microsoft.Extensions.Configuration;

IConfiguration config = ConfigurationHelpers.GetConfiguration();

if (ConfigurationHelpers.TryGetValue<string>(config, "ConnectionStrings:MainConnection", out var cn))
{
		Console.WriteLine(cn);
}
```

### Latest Log File

```csharp
FileInfo? latest = FileHelper.GetLogFileName();
if (latest is not null)
{
		Console.WriteLine(latest.FullName);
}
```

### Caller-Aware Metadata

```csharp
var product = Info.GetProduct(out var caller);
Console.WriteLine(product);
Console.WriteLine($"Called from {caller.TypeName}.{caller.MethodName} at line {caller.LineNumber}");
```

## Notes

- The library assumes predictable file locations for some helpers (for example, `appsettings.json` and `LogFiles`).
- Methods that read files can throw standard I/O and JSON parsing exceptions if files are missing or malformed.