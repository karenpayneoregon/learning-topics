
namespace PreviewFeaturesApp.Classes.Extensions;

/// <summary>
/// Provides a set of extension methods for working with enumerable collections.
/// </summary>
public static class EnumerableExtensions
{
    extension<TSource>(IEnumerable<TSource>) 
    {
        public static IEnumerable<TSource> Combine(IEnumerable<TSource> first, IEnumerable<TSource> second) 
            => first.Concat(second);

        // static extension property:
        public static IEnumerable<TSource> Identity 
            => Enumerable.Empty<TSource>();

    }
}