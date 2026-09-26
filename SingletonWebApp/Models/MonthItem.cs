namespace SingletonWebApp.Models;

/// <summary>
/// Represents a month with its index and name.
/// </summary>
/// <remarks>
/// This record is used to encapsulate information about a month, including its numerical index and name.
/// </remarks>
public record MonthItem(int Index, string Name)
{
    public override string ToString() => Name;
}