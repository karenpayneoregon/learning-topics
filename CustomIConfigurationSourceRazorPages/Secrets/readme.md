# Instructions

To use secrets in your Razor Pages, you need remove the following line in the project file:

```xml
<UserSecretsId>7fba46f3-24f9-4832-a34c-46e91d29f03c</UserSecretsId>
```

- Follow these [instructions](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-9.0&tabs=windows#enable-secret-storage).
- Right click on the project and select `Manage User Secrets`.
- Add the contents of `secrets.json` to the user secrets file.
    - Or modify the `set-secrets.bat` file to match your project location and run it. Then run set-secrets.bat.