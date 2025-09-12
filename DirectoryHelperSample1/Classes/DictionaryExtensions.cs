namespace DirectoryHelperSample1.Classes;

public static class EnumerableExtensions
{
    /// <summary>
    /// Retrieves an element from the end of a sequence based on the specified index.
    /// </summary>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
    /// <param name="source">The sequence to retrieve the element from.</param>
    /// <param name="indexFromEnd">
    /// The zero-based index from the end of the sequence. 
    /// For example, 0 retrieves the last element, 1 retrieves the second-to-last element, and so on.
    /// </param>
    /// <returns>The element at the specified index from the end of the sequence.</returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the <paramref name="source"/> sequence is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="indexFromEnd"/> is negative or greater than or equal to the length of the sequence.
    /// </exception>
    public static T FromEnd<T>(this IEnumerable<T> source, int indexFromEnd)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        var list = source as IList<T> ?? source.ToList();

        return indexFromEnd < 0 || indexFromEnd >= list.Count
            ? throw new ArgumentOutOfRangeException(nameof(indexFromEnd))
            : list[list.Count - 1 - indexFromEnd];
    }
    
    /// <summary>
    /// Attempts to retrieve an element from the end of a sequence based on the specified index.
    /// </summary>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
    /// <param name="source">The sequence to retrieve the element from.</param>
    /// <param name="indexFromEnd">
    /// The zero-based index from the end of the sequence. 
    /// For example, 0 retrieves the last element, 1 retrieves the second-to-last element, and so on.
    /// </param>
    /// <param name="value">
    /// When this method returns, contains the element at the specified index from the end of the sequence, 
    /// if the index is valid; otherwise, the default value for the type of the element.
    /// </param>
    /// <returns>
    /// <c>true</c> if the element at the specified index from the end of the sequence is successfully retrieved; 
    /// otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the <paramref name="source"/> sequence is <c>null</c>.
    /// </exception>
    public static bool TryFromEnd<T>(this IEnumerable<T> source, int indexFromEnd, out T value)
    {
        value = default!;
        if (source == null) return false;

        var list = source as IList<T> ?? source.ToList();

        if (indexFromEnd < 0 || indexFromEnd >= list.Count)
            return false;

        value = list[list.Count - 1 - indexFromEnd];
        return true;
    }
}
