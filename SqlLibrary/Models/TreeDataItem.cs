namespace SqlLibrary.Models;
public class TreeDataItem
{
    public string Database { get; set; }
    public string Catalog { get; set; }
    public string ColumnName { get; set; }
    public override string ToString() => $"{Database}.{Catalog}.{ColumnName}";

}
