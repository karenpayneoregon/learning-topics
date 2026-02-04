#nullable disable

namespace IComparerIEqualityComparerApp.Models;
/// <summary>
/// Represents a product with properties such as ID, name, description, and price.
/// </summary>
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
}