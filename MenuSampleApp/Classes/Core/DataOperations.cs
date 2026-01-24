using ConsoleConfigurationLibrary.Classes;
using Dapper;
using MenuSampleApp.Models;
using Microsoft.Data.SqlClient;

namespace MenuSampleApp.Classes.Core;
/// <summary>
/// Performs data operations.
/// </summary>
internal class DataOperations
{
    private static string MasterConnectionString()
        => "Data Source=.\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;Encrypt=False";

    public static bool ExpressDatabaseExists(string databaseName)
    {
        using var cn = new SqlConnection(MasterConnectionString());
        using var cmd = new SqlCommand($"SELECT DB_ID('{databaseName}'); ", cn);
        cn.Open();
        return cmd.ExecuteScalar() != DBNull.Value;
    }

    /// <summary>
    /// Extracts the initial catalog (database name) from the provided connection string.
    /// </summary>
    /// <param name="connectionString">The connection string from which to extract the initial catalog.</param>
    /// <returns>The name of the initial catalog (database) specified in the connection string.</returns>
    public static string InitialCatalogFromConnectionString(string connectionString)
        => new SqlConnectionStringBuilder(connectionString).InitialCatalog;

    /// <summary>
    /// Retrieves a list of tables and their respective row counts from the specified database.
    /// </summary>
    /// <param name="connectionString">The connection string used to connect to the database.</param>
    /// <returns>A list of <see cref="MenuSampleApp.Models.TableInfo"/> objects, each containing the table name and row count.</returns>
    /// <remarks>
    /// This method queries the system tables to obtain information about all tables in the database,
    /// including their names and the number of rows they contain. The results are ordered by row count
    /// in descending order, and then by table name.
    /// </remarks>
    /// <exception cref="SqlException">Thrown if there is an issue connecting to the database or executing the query.</exception>
    public static List<TableInfo> TablesCount(string connectionString)
    {
        const string query =
            """
            SELECT t.name AS TableName,
                   p.rows AS [RowCount]
            FROM sys.tables t
            INNER JOIN sys.partitions p
                ON t.object_id = p.object_id
            WHERE p.index_id IN (0, 1)
            ORDER BY p.rows DESC, t.name;
            """;

        using var cn = new SqlConnection(connectionString);

        return cn.Query<TableInfo>(query).ToList();
    }

    /// <summary>
    /// Retrieves a list of categories from the database.
    /// </summary>
    /// <returns>A list of <see cref="MenuSampleApp.Models.Categories"/> objects, each containing the category ID and name.</returns>
    /// <remarks>
    /// This method executes a query to fetch all categories from the `dbo.Categories` table.
    /// </remarks>
    /// <exception cref="SqlException">Thrown if there is an issue connecting to the database or executing the query.</exception>
    public static List<Categories> GetCategories()
    {
        using SqlConnection cn = new(AppConnections.Instance.MainConnection);
        return cn.Query<Categories>("SELECT CategoryID,CategoryName FROM dbo.Categories;").ToList();
    }

}
