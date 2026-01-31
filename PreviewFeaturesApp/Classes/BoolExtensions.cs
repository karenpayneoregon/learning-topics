namespace PreviewFeaturesApp.Classes
{
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
}
