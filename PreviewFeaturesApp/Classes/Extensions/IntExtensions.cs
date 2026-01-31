namespace PreviewFeaturesApp.Classes.Extensions;

/// <summary>
/// Provides extension methods for <see cref="int"/> type.
/// </summary>
/// <remarks>
/// This class contains static methods that extend the functionality of the <see cref="int"/> type.
/// </remarks>
public static class IntExtensions
{
    extension(int number)
    {
        public void Increment()
            => number++;
    }


}