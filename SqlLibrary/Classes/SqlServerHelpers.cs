using System.Collections.Immutable;
using Dapper;
using Microsoft.Data.SqlClient;
using SqlLibrary.Models;

namespace SqlLibrary.Classes;

public class SqlServerHelpers
{
    /// <summary>
    /// Retrieves the table dependencies for a given database.
    /// </summary>
    /// <param name="connectionString">The connection string to the database.</param>
    /// <returns>A read-only list of dependency group items.</returns>
    public static async Task<IReadOnlyList<DependencyGroupItem>> TableDependencies(string connectionString)
    {
        await using var cn = new SqlConnection(connectionString);
        await using var cmd = new SqlCommand(SqlStatements.DependenciesStatement, cn);

        await cn.OpenAsync();

        var reader = await cmd.ExecuteReaderAsync();
        List<DependsItem> list = [];

        while (reader.Read())
        {
            list.Add(new DependsItem()
            {
                ParentTable = reader.GetString(0),
                ParentColumn = reader.GetString(1),
                ReferenceTable = reader.GetString(2),
                ReferenceColumn = reader.GetString(3)
            });
        }

        return list
            .GroupBy(item => item.ParentTable)
            .Select(item => new DependencyGroupItem(
                item.Key,
                item.ToList().OrderBy(x => x.ReferenceTable).ToList()))
            .ToImmutableList();
    }

    public static async Task<IReadOnlyList<DependencyGroupItem>> TableDependenciesAsync(string connectionString)
    {
        await using var cn = new SqlConnection(connectionString);

        return (await cn.QueryAsync<DependsItem>(SqlStatements.DependenciesStatement)).ToList()
            .GroupBy(item => item.ParentTable)
            .Select(g => new DependencyGroupItem(
                g.Key,
                g.OrderBy(x => x.ReferenceTable).ToList()))
            .ToImmutableList();
    }
        
}