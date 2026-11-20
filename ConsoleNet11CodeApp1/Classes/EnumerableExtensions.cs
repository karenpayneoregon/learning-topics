namespace ConsoleNet11CodeApp1.Classes;

/// <summary>
/// Provides extension methods for <see cref="System.Collections.Generic.IEnumerable{T}"/>.
/// </summary>
public static class EnumerableExtensions
{
    extension<T>(IEnumerable<T> sender)
    {
        public int Count => sender.Count();

        /// <summary>
        /// Gets the element at the specified index in the enumerable sequence.
        /// </summary>
        /// <param name="index">The zero-based index of the element to retrieve.</param>
        /// <returns>The element at the specified index in the enumerable sequence.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Thrown if the <paramref name="index"/> is less than 0 or greater than or equal to the number of elements in the sequence.
        /// </exception>
        /// <remarks>
        /// Some developers might want a bounds check
        /// </remarks>
        public T this[int index] => sender.ElementAt(index);
    }
}