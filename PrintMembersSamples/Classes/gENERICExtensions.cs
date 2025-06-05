using System.Diagnostics;

namespace PrintMembersSamples.Classes;

internal static class GenericExtensions
{
    /// <summary>
    /// Determines whether the specified value is within the inclusive range defined by the start and end values.
    /// </summary>
    /// <remarks>This method uses the <see cref="IComparable{T}.CompareTo(T)"/> method to compare the values,
    /// ensuring compatibility with any type that implements <see cref="IComparable{T}"/>.</remarks>
    /// <typeparam name="T">The type of the value to compare. Must implement <see cref="IComparable{T}"/>.</typeparam>
    /// <param name="value">The value to test for inclusion in the range.</param>
    /// <param name="start">The lower bound of the range, inclusive.</param>
    /// <param name="end">The upper bound of the range, inclusive.</param>
    /// <returns><see langword="true"/> if <paramref name="value"/> is greater than or equal to <paramref name="start"/> and less
    /// than or equal to <paramref name="end"/>; otherwise, <see langword="false"/>.</returns>
    [DebuggerStepThrough]
    public static bool IsBetween<T>(this T value, T start, T end) where T : IComparable<T> => 
        value.CompareTo(start) >= 0 && value.CompareTo(end) <= 0;
}

