using System.Collections.Generic;
using System.Linq;

namespace PreviewFeaturesApp.Classes;

public static class EnumerableExtensions
{
    extension<TSource>(IEnumerable<TSource>) 
    {
        // static extension method:
        public static IEnumerable<TSource> Combine(IEnumerable<TSource> first, IEnumerable<TSource> second) 
            => first.Concat(second);

        // static extension property:
        public static IEnumerable<TSource> Identity 
            => Enumerable.Empty<TSource>();

    }
}


public static class IntExtensions
{
    extension(int number)
    {
        public void Increment()
            => number++;
    }


}