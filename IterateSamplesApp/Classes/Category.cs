namespace IterateSamplesApp.Classes;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public override string ToString() => $"{Id,-3} {Name}";
}


