using System.Text.Json;

namespace AddInMemoryCollectionSample.Classes
{
    /// <summary>
    /// Provides functionality to create application settings in JSON format.
    /// </summary>
    /// <remarks>
    /// This class generates a JSON representation of application settings, including connection strings 
    /// and layout configurations. The JSON is formatted for readability and can be used for configuration purposes.
    /// </remarks>
    public class AppSettingsCreator
    {
        /// <summary>
        /// Creates a JSON string representing application settings, including connection strings and layout configurations.
        /// </summary>
        /// <returns>
        /// A JSON-formatted string containing application settings such as connection strings and layout configurations.
        /// </returns>
        /// <remarks>
        /// The generated JSON includes a connection string for the main database and layout settings for the header, title, and footer.
        /// The JSON is formatted with indentation for readability.
        ///
        /// The generated JSON string can be used to create an appsettings.json file for configuration purposes.
        /// 
        /// </remarks>
        public static string CreateAppSettingsJson()
        {
            var appSettings = new
            {
                ConnectionStrings = new
                {
                    MainConnection = "Server=(localdb)\\MSSQLLocalDB;Database=NorthWind2024;Trusted_Connection=True"
                },
                Layout = new
                {
                    Header = "Visible",
                    Title = "Some title",
                    Footer = "Hidden"
                }
            };

            /*
             *
             */
            var json = JsonSerializer.Serialize(appSettings, Indented);

            return json;

        }

        public static JsonSerializerOptions Indented => new() { WriteIndented = true };
    }
}
