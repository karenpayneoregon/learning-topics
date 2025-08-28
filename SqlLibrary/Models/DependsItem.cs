namespace SqlLibrary.Models;

public class DependsItem
{
    public string ParentTable { get; set; }
    public string ParentColumn { get; set; }
    public string ReferenceTable { get; set; }
    public string ReferenceColumn { get; set; }
}

