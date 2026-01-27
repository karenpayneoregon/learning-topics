using Microsoft.Extensions.Configuration;
using System.Reflection;
using SecretBasicApp.Models;


namespace SecretBasicApp.Classes.Core;

/// <summary>
/// Provides functionality to read configuration settings and secrets for the application.
/// </summary>
/// <remarks>
/// This class is implemented as a singleton to ensure a single instance is used throughout the application.
/// It supports reading configuration sections, individual properties, connection strings, and mail settings.
/// Additionally, it determines the current working environment (e.g., Development or Production).
/// </remarks>
public sealed class SecretReader
{
    private static readonly Lazy<SecretReader> _instance = new(() => new());

    public static SecretReader Instance => _instance.Value;
    private readonly IConfigurationRoot _configuration;
    private readonly EnvironmentType _environment;

    /// <summary>
    /// Initializes a new instance of the <see cref="SecretReader"/> class.
    /// </summary>
    /// <remarks>
    /// This constructor is private to enforce the singleton pattern.
    /// It initializes the configuration settings by loading JSON files, environment variables,
    /// and user secrets (in the Development environment).
    /// </remarks>
    private SecretReader()
    {
        _environment = EnvironmentType.Development; // GetWorkingEnvironment();

        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{_environment}.json", optional: true)
            .AddEnvironmentVariables();

        if (_environment == EnvironmentType.Development)
        {
            builder.AddUserSecrets(Assembly.GetExecutingAssembly());
        }

        _configuration = builder.Build();
    }

    public T ReadSection<T>(string sectionName)
        => _configuration.GetSection(sectionName).Get<T>();

    public T ReadProperty<T>(string sectionName, string name)
        => _configuration.GetSection(sectionName).GetValue<T>(name);

    public string ConnectionString
        => ReadProperty<string>(nameof(ConnectionStrings), nameof(ConnectionStrings.DefaultConnection));

    public MailSettings MailSettings
        => ReadSection<MailSettings>(nameof(MailSettings));

    public Login Login
        => ReadSection<Login>(nameof(Login));

    public EnvironmentType Environment => _environment;

    public static EnvironmentType GetWorkingEnvironment() =>
        System.Environment.GetEnvironmentVariable("CONSOLE_ENVIRONMENT") switch
        {
            "Development" => EnvironmentType.Development,
            "Production" => EnvironmentType.Production,
            _ => EnvironmentType.Development
        };

}