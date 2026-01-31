using System.Numerics;

namespace PreviewFeaturesApp.Classes.Extensions;
public static class NumericExtensions
{
    /// <summary>
    /// Provides extension methods for numeric operations on a sequence of elements of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The numeric type of the elements in the sequence. Must implement <see cref="System.Numerics.INumber{T}"/>.
    /// </typeparam>
    extension<T>(IEnumerable<T> source) where T : INumber<T>
    {
        public IEnumerable<T> OnlyPositive => source.Where(x => x > T.Zero);
    }
}
