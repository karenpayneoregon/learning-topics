# About

The purpose of this project is to recommend not using tables but instead using the Bootstrap responsive structure, as using tables, screen readers announce more details than needed, and in some cases, missing details.

## Returning to calling page after editing an entity

Since two pages use the edit page, the edit page needs to know where to return after saving or canceling. This is done by passing a `returnUrl` parameter in the query string. This requires `builder.Services.AddHttpContextAccessor();` in `Program.cs` and injecting `IHttpContextAccessor` in the edit page.

## Database

See script in the `Scripts` folder.


## Miscellaneous Notes

- In the project file
    - Language is set to English (United States) if not specified otherwise.
    - The connection string is stored in `appsettings.json` file.
    - See `MakeLogDir` target for creating a log directory.
    - EF Core logging uses NuGet package [EntityCoreFileLogger](https://www.nuget.org/packages/EntityCoreFileLogger/1.0.0?_src=template).

## Bootstrap Icons

:diamonds: All icons are installed while for a real application you would only install the ones you need.
