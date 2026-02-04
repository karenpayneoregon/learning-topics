#nullable disable

using IComparerIEqualityComparerApp.Models;

namespace IComparerIEqualityComparerApp.Classes.Comparers;

/// <summary>
/// Provides an equality comparer for <see cref="Person"/> objects, comparing them
/// based on their first name, last name, and birthdate.
/// </summary>
/// <remarks>
/// This comparer performs case-insensitive comparisons for the first name and last name
/// and considers the birthdate as part of the equality check. It is useful for scenarios
/// where distinct <see cref="Person"/> objects need to be identified based on these properties.
/// </remarks>
public sealed class FirstNameLastNameBirthDateComparer : IEqualityComparer<Person>
{
    
    /// <summary>
    /// Determines whether two <see cref="Person"/> objects are equal by comparing their
    /// first name, last name, and birthdate.
    /// </summary>
    /// <param name="left">The first <see cref="Person"/> to compare.</param>
    /// <param name="right">The second <see cref="Person"/> to compare.</param>
    /// <returns>
    /// <c>true</c> if the specified <see cref="Person"/> objects are equal; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// This method performs a case-insensitive comparison for the first name and last name
    /// and checks for equality of the birthdate. It is designed to ensure that two <see cref="Person"/>
    /// objects are considered equal if these three properties match.
    /// </remarks>
    public bool Equals(Person left, Person right)
    {
        if (ReferenceEquals(left, right)) return true;
        if (left is null || right is null) return false;
        if (left.GetType() != right.GetType()) return false;

        return string.Equals(left.FirstName, right.FirstName, StringComparison.CurrentCultureIgnoreCase)
               && string.Equals(left.LastName, right.LastName, StringComparison.CurrentCultureIgnoreCase)
               && left.BirthDate.Equals(right.BirthDate);
    }

    /// <summary>
    /// Returns a hash code for the specified <see cref="Person"/> object based on its
    /// first name, last name, and birthdate.
    /// </summary>
    /// <param name="obj">The <see cref="Person"/> object for which to generate a hash code.</param>
    /// <returns>
    /// A hash code for the specified <see cref="Person"/> object, suitable for use in hashing algorithms
    /// and data structures such as a hash table.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if the <paramref name="obj"/> parameter is <c>null</c>.
    /// </exception>
    /// <remarks>
    /// This method generates a hash code by combining the hash codes of the first name, last name,
    /// and birthdate of the <see cref="Person"/> object. The first name and last name are compared
    /// in a case-insensitive manner.
    /// </remarks>
    public int GetHashCode(Person obj)
    {
        var hashCode = new HashCode();
        hashCode.Add(obj.FirstName, StringComparer.CurrentCultureIgnoreCase);
        hashCode.Add(obj.LastName, StringComparer.CurrentCultureIgnoreCase);
        hashCode.Add(obj.BirthDate);
        return hashCode.ToHashCode();
    }
}