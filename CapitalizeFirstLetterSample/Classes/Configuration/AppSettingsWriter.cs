using CapitalizeFirstLetterSample.Classes.Core;
using CapitalizeFirstLetterSample.Models;
using Serilog;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace CapitalizeFirstLetterSample.Classes.Configuration
{
    public static class AppSettingsWriter
    {
        /// <summary>
        /// Updates the main database connection string in the application's configuration file.
        /// </summary>
        /// <remarks>
        /// This method modifies the "appsettings.json" file to update the "MainConnection" property
        /// under the "ConnectionStrings" section. It also updates the <see>
        ///     <cref>DataConnections.Instance.MainConnection</cref>
        /// </see>
        /// property to reflect the new connection string.
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the "appsettings.json" file contains invalid JSON format.
        /// </exception>
        public static void UpdateMainConnection()
        {
            var filePath = "appsettings.json";

            var json = File.ReadAllText(filePath);

            if (JsonNode.Parse(json) is not JsonObject jsonNode)
            {
                var error = new InvalidOperationException("Invalid JSON format.");
                Log.Fatal(error, "Failed to parse appsettings.json");
                throw error;
            }

            jsonNode[nameof(ConnectionStrings)] = new JsonObject
            {
                [nameof(ConnectionStrings.MainConnection)] =
                    "Data Source=.\\SQLEXPRESS;Initial Catalog=NorthWind;Integrated Security=True;Encrypt=False"

            };

            File.WriteAllText(filePath, jsonNode.ToJsonString(Indented));

            DataConnections.Instance.MainConnection =
                jsonNode[nameof(ConnectionStrings)]?[nameof(ConnectionStrings.MainConnection)]?.ToString() ?? string.Empty;
        }

        public static JsonSerializerOptions Indented => new() { WriteIndented = true };
    }

}
