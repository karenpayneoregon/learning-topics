using IComparerIEqualityComparerApp.Models;

namespace IComparerIEqualityComparerApp.Classes.Comparers;

/// <summary>
/// Provides a comparer for <see cref="Person"/> objects, enabling comparison based on their last names.
/// </summary>
/// <remarks>
/// This class implements the <see cref="IComparer{T}"/> interface to compare two <see cref="Person"/> objects.
/// The comparison is performed using the <see cref="Person.LastName"/> property, with an ordinal string comparison.
/// </remarks>
public class PersonComparer : IComparer<Person>
{
    /// <summary>
    /// Compares two <see cref="Person"/> objects based on their last names.
    /// </summary>
    /// <param name="left">The first <see cref="Person"/> object to compare.</param>
    /// <param name="right">The second <see cref="Person"/> object to compare. Can be <c>null</c>.</param>
    /// <returns>
    /// A signed integer that indicates the relative values of the <paramref name="left"/> and <paramref name="right"/> objects:
    /// <list type="bullet">
    /// <item>
    /// <description>Less than zero: <paramref name="left"/> is less than <paramref name="right"/>.</description>
    /// </item>
    /// <item>
    /// <description>Zero: <paramref name="left"/> is equal to <paramref name="right"/>.</description>
    /// </item>
    /// <item>
    /// <description>Greater than zero: <paramref name="left"/> is greater than <paramref name="right"/>.</description>
    /// </item>
    /// </list>
    /// </returns>
    /// <remarks>
    /// The comparison is performed using the <see cref="Person.LastName"/> property with an ordinal string comparison.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="left"/> is <c>null</c>.
    /// </exception>
    public int Compare(Person? left, Person? right) 
        => string.Compare(left.LastName, right.LastName, StringComparison.Ordinal);
}