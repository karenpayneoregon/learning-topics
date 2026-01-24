namespace MenuSampleApp.Models;

public class Categories
{
    public int CategoryId { get; set; }
    public required string CategoryName { get; set; }
    public override string ToString() => CategoryName;
}