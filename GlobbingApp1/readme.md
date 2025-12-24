# File globbing in .NET

An example project demonstrating [file globbing](https://learn.microsoft.com/en-us/dotnet/core/extensions/file-globbing).

Base folder `C:\Users\CurrentUser`

Get files matching a pattern:
```csharp
string[] include = ["**/Doc*.docx", "**/Employee*.sql"];
```

Exclude files matching a pattern:

```csharp
string[] exclude = 
[
    "**/Doc2*.docx",
    "**/DesktopStuff/**",                   // exclude DesktopStuff folder
    "**/SQL Server Management Studio/**",   // exclude SQL Server Management Studio folder
    "**/My Music/**",                       // exclude My Music folder
    "**/My Pictures/**",                    // exclude My Pictures folder
    "**/My Videos/**"                       // exclude My Videos folder
];
```
- Third we only want `.sql` in parent folder `C:\Users\CurrentUser`:
- Last three ignore along with preventing access denied errors.

## Reading options from JSON file

You can also read the globbing options from a JSON configuration file, for example `appsettings.json` or in this case `GlobbingOptions.json`:

```json
{
  "FileGlobbingOptions": {
    "Include": [ "**/Doc*.docx", "**/Employee*.sql" ],
    "Exclude": [
      "**/Doc2*.docx",
      "**/DesktopStuff/**",
      "**/SQL Server Management Studio/**",
      "**/My Music/**",
      "**/My Pictures/**",
      "**/My Videos/**"
    ]
  }
}
```