namespace PreviewFeaturesApp.Classes.Extensions;

/// <summary>
/// Provides extension methods for the <see cref="bool"/> type.
/// </summary>
public static class BoolExtensions
{
    extension(bool value)
    {
        public string IsEmpty() =>
            value switch
            {
                true => "Yes is empty",
                _ => "No is not empty"
            };

        public string ToYesNo() =>
            value switch
            {
                true => "Yes",
                _ => "No"
            };
    }
}