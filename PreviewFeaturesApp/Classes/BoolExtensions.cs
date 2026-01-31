namespace PreviewFeaturesApp.Classes
{
    public static class BoolExtensions
    {
        public static string ToYesNo(this bool value) =>
            value switch
            {
                true => "Yes",
                _ => "No"
            };

        extension(bool value)
        {
            public string IsEmpty() =>
                value switch
                {
                    true => "Yes is empty",
                    _ => "No is not empty"
                };
        }
    }
}
