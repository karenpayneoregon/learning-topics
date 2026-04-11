using Microsoft.Extensions.DependencyInjection;
using ProcessOrdersApp.Models.Configuration;

namespace ProcessOrdersApp.Classes.Configuration;
internal class ApplicationConfiguration
{
    /// <summary>
    /// Sets up the services for connection string and should mock data be used
    /// </summary>
    /// <returns>ServiceCollection</returns>
    public static ServiceCollection ConfigureServices()
    {
        static void ConfigureService(IServiceCollection services)
        {

            services.Configure<ConnectionStrings>(Config.Configuration.JsonRoot()
                .GetSection(nameof(ConnectionStrings)));

            services.Configure<EntityConfiguration>(Config.Configuration.JsonRoot()
                .GetSection(nameof(EntityConfiguration)));

            services.Configure<FileConfiguration>(Config.Configuration.JsonRoot()
                .GetSection(nameof(FileConfiguration)));

            services.AddTransient<SetupServices>();
        }

        var services = new ServiceCollection();
        ConfigureService(services);

        return services;

    }
}


