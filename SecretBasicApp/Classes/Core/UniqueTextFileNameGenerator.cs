namespace SecretBasicApp.Classes.Core;

public static class UniqueTextFileNameGenerator
{
    /// <summary>
    /// Generates a unique text file name based on the specified directory and prefix.
    /// </summary>
    /// <param name="directory">The directory where the file will be located.</param>
    /// <param name="prefix">
    /// An optional prefix for the file name. Defaults to "file" if not specified.
    /// </param>
    /// <returns>
    /// A string representing the full path of the uniquely generated text file name.
    /// </returns>
    /// <remarks>
    /// The generated file name includes the specified prefix, a UTC timestamp, and a GUID, 
    /// ensuring uniqueness. The file name has a ".txt" extension.
    /// </remarks>
    public static string Create(string directory, string prefix = "file")
    {

        string timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
        string guid = Guid.NewGuid().ToString("N");

        string fileName = $"{prefix}_{timestamp}_{guid}.txt";

        return Path.Combine(directory, fileName);
    }
}