namespace GetGlobalNuGetPackages.Models;

public class PackageGroup(string name, int count, List<string> versions)
{
    public string Name { get; } = name;
    public int Count { get; } = count;
    public List<string> Versions { get; } = versions;

    public override string ToString()
    {
        return $"{{ Name = {Name}, Count = {Count}, Versions = {Versions} }}";
    }

    public override bool Equals(object value)
    {
        return value is PackageGroup other && EqualityComparer<string>.Default.Equals(other.Name, Name) && EqualityComparer<int>.Default.Equals(other.Count, Count) && EqualityComparer<List<string>>.Default.Equals(other.Versions, Versions);
    }

    public override int GetHashCode()
    {
        var hash = 0x7a2f0b42;
        hash = (-1521134295 * hash) + EqualityComparer<string>.Default.GetHashCode(Name);
        hash = (-1521134295 * hash) + EqualityComparer<int>.Default.GetHashCode(Count);
        return (-1521134295 * hash) + EqualityComparer<List<string>>.Default.GetHashCode(Versions);
    }
}