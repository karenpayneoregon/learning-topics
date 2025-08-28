namespace SqlLibrary.Models;

public record DependencyGroupItem(string TableName, List<DependsItem> List)
{
    public override string ToString() => $"{{ TableName = {TableName}, List = {List} }}";
}