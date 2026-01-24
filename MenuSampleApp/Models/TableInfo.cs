namespace MenuSampleApp.Models;

/// <summary>
/// Represents information about a database table, including its schema, name, and row count.
/// </summary>
public class TableInfo
{
    public required string TableSchema { get; set; }
    public required string TableName { get; set; }
    public required int RowCount { get; set; }
}