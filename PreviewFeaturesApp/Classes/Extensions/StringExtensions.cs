using System.Text;

namespace PreviewFeaturesApp.Classes.Extensions;
/// <summary>
/// Provides extension methods for <see cref="string"/> to enhance its functionality.
/// </summary>
/// <remarks>
/// This class contains static methods that extend the capabilities of the <see cref="string"/> type.
/// </remarks>
public static class StringExtensions
{

    extension(string sender)
    {
        public string CapitalizeFirstLetter()
            => string.IsNullOrEmpty(sender) ? sender : $"{char.ToUpper(sender[0])}{sender[1..].ToLower()}";

        public bool IsInteger()
        {
            foreach (var c in sender)
                if (c is < '0' or > '9') return false;

            return true;
        }
        
        public bool IsEmptyField => string.IsNullOrEmpty(sender);

    }

    /// <param name="source">The collection of elements to join. If <c>null</c>, an empty string is returned.</param>
    /// <typeparam name="T">The type of the elements in the <paramref name="source"/> collection.</typeparam>
    extension<T>(IEnumerable<T> source)
    {
        /// <summary>
        /// Joins the elements of the specified <paramref name="source"/> collection into a single string, 
        /// using the specified <paramref name="separator"/> between most elements and the specified 
        /// <paramref name="token"/> before the last element.
        /// </summary>
        /// <param name="separator">
        /// The string to use as a separator between elements, except for the last element. 
        /// Defaults to <c>", "</c> if not specified.
        /// </param>
        /// <param name="token">
        /// The string to use before the last element in the joined result. 
        /// Defaults to <c>"and"</c> if not specified.
        /// </param>
        /// <returns>
        /// A string that consists of the elements in <paramref name="source"/> joined by the specified 
        /// <paramref name="separator"/> and <paramref name="token"/>. Returns an empty string if 
        /// <paramref name="source"/> is empty or <c>null</c>.
        /// </returns>
        public string JoinWithLastSeparator(string separator = ", ", string token = "and")
        {
            if (source is null) return string.Empty;

            separator ??= ", ";
            token ??= "and";

            using var ge = source.GetEnumerator();
            if (!ge.MoveNext()) return string.Empty;

            var first = ge.Current?.ToString() ?? string.Empty;
            if (!ge.MoveNext()) return first;

            StringBuilder sb = new(first);
            var prev = ge.Current?.ToString() ?? string.Empty;

            while (ge.MoveNext())
            {
                sb.Append(separator);
                sb.Append(prev);
                prev = ge.Current?.ToString() ?? string.Empty;
            }

            sb.Append(' ')
                .Append(token)
                .Append(' ')
                .Append(prev);

            return sb.ToString();
        }
    }
}
