namespace SqlLibrary.Models;
public class TreeDataItem
{
    public required string Database { get; set; }
    public required string Catalog { get; set; }
    public required string ColumnName { get; set; }
    public override string ToString() => $"{Database}.{Catalog}.{ColumnName}";

}
