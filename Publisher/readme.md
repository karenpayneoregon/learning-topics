# About

Article: [NuGet Package Publisher](https://dev.to/karenpayneoregon/how-to-nuget-local-feeds-2nci)

Utility to publish a NuGet package to a designated local feed.

Uses `appsettings.json` which resides in the executable folder for configuration.

```json
{
  "PackageLocation": "C:\\OED\\Dotnetland\\NuGetLocal"
}
```

- **NuGetExecutable** location of nuget.exe (not needed for this branch)
- **PackageLocation** location of local feed


---

## ClassProjects section



```json
{
  "NuGetExecutable": "C:\\OED\\Dotnetland\\CodeStash\\NuGetStuff\\nuget.exe",
  "PackageLocation": "C:\\OED\\NuGetLocal",
  "ClassProjects": [
    {
      "Path": "C:\\OED\\DotnetLand\\VS2022\\ProjectTemplates2025\\SpectreConsoleLibrary"
    },
    {
      "Path": "C:\\OED\\DotnetLand\\VS2022\\BogusTrainingSolution\\BogusLibrary"
    }
  ]
}

