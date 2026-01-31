namespace PreviewFeaturesApp.Classes;
public static class GenericIterators
{
    /// <summary>
    /// Iterates over a collection of items provided as parameters and prints each item
    /// to the console. If an item is <c>null</c>, it prints an empty string instead.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    /// <param name="values">The collection of items to iterate over, provided as a parameter array.</param>
    public static void Iterate<T>(params T[] values) =>
        values.Iterate(x => x?.ToString() ?? "");

    /// <param name="values">The collection of items to iterate over.</param>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    extension<T>(IEnumerable<T> values)
    {
        /// <summary>
        /// Iterates over a collection of items and applies a formatter function to each item.
        /// The formatted result is then printed to the console along with the item's index.
        /// </summary>
        /// <param name="formatter">
        /// A function that takes an item of type <typeparamref name="T"/> and returns a string
        /// representation of the item.
        /// </param>
        public void Iterate(Func<T, string> formatter)
        {
            foreach (var (index, item) in values.Index())
                Console.WriteLine($"{index,-5}{formatter(item)}");
        }
    }
}


