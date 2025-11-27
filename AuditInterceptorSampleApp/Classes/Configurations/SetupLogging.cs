using Microsoft.Extensions.Configuration;
using Serilog;

namespace AuditInterceptorSampleApp.Classes.Configurations;
internal class SetupLogging
{
    public static void Development()
    {
        var fileName = Config.JsonRoot().GetSection(nameof(SerilogSection)).Get<SerilogSection>().FileName;
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()

            .WriteTo.File(fileName,
                rollingInterval: RollingInterval.Minute,
                outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}")
            .CreateLogger();

    }
}