namespace IComparerIEqualityComparerApp.Classes.SystemCode;
internal static class Extensions
{
    /// <summary>
    /// Adds a range of items to the <see cref="SortedSet{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of items in the <see cref="SortedSet{T}"/>.</typeparam>
    /// <param name="source">The <see cref="SortedSet{T}"/> to add the items to.</param>
    /// <param name="items">The collection of items to add.</param>
    /// <returns><c>true</c> if all items were successfully added; otherwise, <c>false</c>.</returns>
    public static bool AddRange<T>(this SortedSet<T> source, IEnumerable<T> items)
    {
        bool allAdded = true;
        foreach (var item in items)
        {
            allAdded = allAdded & source.Add(item);
        }
        
        return allAdded;
    }

    public static string? CapitalizeFirstLetter(this string? input)
        => string.IsNullOrWhiteSpace(input) ?
            input : char.ToUpper(input[0]) + input.AsSpan(1).ToString();
}
