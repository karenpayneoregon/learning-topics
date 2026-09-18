namespace CapitalizeFirstLetterSample.Classes;

internal static class Extensions
{
    /// <param name="source">The <see cref="SortedSet{T}"/> to add the items to.</param>
    /// <typeparam name="T">The type of items in the <see cref="SortedSet{T}"/>.</typeparam>
    extension<T>(SortedSet<T> source)
    {
        /// <summary>
        /// Adds a range of items to the <see cref="SortedSet{T}"/>.
        /// </summary>
        /// <param name="items">The collection of items to add.</param>
        /// <returns><c>true</c> if all items were successfully added; otherwise, <c>false</c>.</returns>
        public bool AddRange(IEnumerable<T> items)
        {
            bool allAdded = true;
            foreach (var item in items)
            {
                allAdded = allAdded & source.Add(item);
            }

            return allAdded;
        }
    }

    extension(string? source)
    {
        public string? CapitalizeFirstLetter()
            => string.IsNullOrWhiteSpace(source) ?
                source : char.ToUpper(source[0]) + source.AsSpan(1).ToString().ToLower();
    }
}
