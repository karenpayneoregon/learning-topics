using Microsoft.Extensions.Configuration;
using SingletonLibrary.Classes;

namespace SingletonLibrary;

#nullable disable

public sealed class EnvironmentSettings
{
    private static readonly Lazy<EnvironmentSettings> Lazy = new(() => new EnvironmentSettings());
    public static EnvironmentSettings Instance => Lazy.Value;

    public string MainConnection { get; set; }

    public static IConfigurationRoot Configuration { get; private set; }

    private EnvironmentSettings()
    {
        Configuration = DataOperations.ConfigurationBuilder().Build();
        MainConnection = Configuration.GetConnectionString("MainConnection");
    }
}