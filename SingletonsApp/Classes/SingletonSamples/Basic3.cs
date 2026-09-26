using Microsoft.Extensions.Configuration;
using SingletonsApp.Classes.Configuration;

namespace SingletonsApp.Classes.SingletonSamples;

#nullable disable

public sealed class Basic3
{
    private static readonly Lazy<Basic3> Lazy = new(() => new Basic3());
    public static Basic3 Instance => Lazy.Value;

    public string MainConnection { get; set; }

    public static IConfigurationRoot Configuration { get; private set; }

    private Basic3()
    {
        Configuration = DataOperations.ConfigurationBuilder().Build();
        MainConnection = Configuration.GetConnectionString("MainConnection");
    }
}