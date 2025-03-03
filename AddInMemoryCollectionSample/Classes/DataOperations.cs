using Microsoft.Data.SqlClient;
using System.Data;
using AddInMemoryCollectionSample.Models;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace AddInMemoryCollectionSample.Classes
{
    public class DataOperations
    {
        /// <summary>
        /// Retrieves configuration settings from a database and returns them as a dictionary.
        /// </summary>
        /// <remarks>
        /// This method queries a database table containing configuration settings with columns for 
        /// section, key, and value. The keys are formatted as "Section:Key" to support hierarchical 
        /// configuration structures.
        /// </remarks>
        /// <returns>
        /// A <see cref="Dictionary{TKey, TValue}"/> where the keys are strings representing 
        /// configuration keys (formatted as "Section:Key") and the values are the corresponding 
        /// configuration values.
        /// </returns>
        public static Dictionary<string, string> ReadFromDatabase()
        {
            /*
             * Config.Configuration.JsonRoot() is equivalent to ConfigurationHelpers.configurationBuilder()
             * Found in NuGet package ConsoleConfigurationLibrary created by Karen Payne
             */
            var connectionStrings = Config.Configuration.JsonRoot()
                .GetSection(nameof(ConnectionStrings))
                .Get<ConnectionStrings>();

            using IDbConnection db = new SqlConnection(connectionStrings.MainConnection);
            string sql = """
                         SELECT Section + ':' + [Key] AS [Key], Value 
                         FROM dbo.Settings;
                         """;

            return db.Query<(string Key, string Value)>(sql)
                .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
