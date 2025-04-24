#nullable enable
namespace StringsBetweenQuotesExample.Models;

public class Container<T>
{
    public T? Value { get; set; }
    public Index StartIndex { get; set; }
    public int Index { get; set; }
    public Index EndIndex { get; set; }
}

public record Month(int Id, string Name)
{
    public override string ToString() => $"{{ Name = {Name}, Id = {Id} }}";
}