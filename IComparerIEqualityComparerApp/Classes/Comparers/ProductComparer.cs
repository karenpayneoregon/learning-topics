using IComparerIEqualityComparerApp.Models;

namespace IComparerIEqualityComparerApp.Classes.Comparers;
public class ProductComparer : IEqualityComparer<Product>
{
    public bool Equals(Product? left, Product? right)
    {
        if (ReferenceEquals(left, right)) return true;
        if (left is null || right is null) return false;
        if (left.GetType() != right.GetType()) return false;


        return left.Name == right.Name && left.Description == right.Description;
    }
    public int GetHashCode(Product obj)
    {
        if (obj is null) return 0;

        var hashProductName = obj.Name == null ? 0 : obj.Name.GetHashCode();
        var hashProductDescription = obj.Description == null ? 0 : obj.Description.GetHashCode();

        return hashProductName ^ hashProductDescription;
    }
}