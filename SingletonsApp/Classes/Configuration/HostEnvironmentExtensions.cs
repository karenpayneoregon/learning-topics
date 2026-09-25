using Microsoft.Extensions.Hosting;

namespace SingletonsApp.Classes.Configuration;

/// <summary>
/// Provides extension methods for the <see cref="Microsoft.Extensions.Hosting.IHostEnvironment"/> interface 
/// to determine the current hosting environment.
/// </summary>
public static class HostEnvironmentExtensions
{
    extension(IHostEnvironment environment)
    {
        /// <summary>
        /// Determines whether the current hosting environment name is set to "Development".
        /// </summary>
        /// <returns>
        /// <c>true</c> if the current hosting environment name is "Development"; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown if the <paramref name="environment"/> is <c>null</c>.
        /// </exception>
        public bool CheckIsDevelopmentEnvironment() => environment.IsDevelopment();

        /// <summary>
        /// Determines whether the current hosting environment name is set to "Staging".
        /// </summary>
        /// <returns>
        /// <c>true</c> if the current hosting environment name is "Staging"; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown if the <paramref name="environment"/> is <c>null</c>.
        /// </exception>
        public bool CheckIsStagingEnvironment() => environment.IsStaging();

        /// <summary>
        /// Determines whether the current hosting environment name is set to "Production".
        /// </summary>
        /// <returns>
        /// <c>true</c> if the current hosting environment name is "Production"; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown if the <paramref name="environment"/> is <c>null</c>.
        /// </exception>
        public bool CheckIsProductionEnvironment() => environment.IsProduction();
        
        /// <summary>
        /// Prints details about the current hosting environment and a specified database connection string to the console.
        /// </summary>
        /// <param name="databaseConnectionString">
        /// The database connection string to be displayed.
        /// </param>
        /// <remarks>
        /// This method outputs the following information:
        /// <list type="bullet">
        /// <item><description>The name of the current hosting environment.</description></item>
        /// <item><description>The application name.</description></item>
        /// <item><description>The content root path.</description></item>
        /// <item><description>The provided database connection string.</description></item>
        /// </list>
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown if the <paramref name="environment"/> is <c>null</c>.
        /// </exception>
        public void Print(string databaseConnectionString)
        {
            Console.WriteLine($"       Current Environment: {environment.EnvironmentName}");
            Console.WriteLine($"          Application Name: {environment.ApplicationName}");
            Console.WriteLine($"         Content Root Path: {environment.ContentRootPath}");
            Console.WriteLine($"Database Connection String: {databaseConnectionString}");
        }
    }
}