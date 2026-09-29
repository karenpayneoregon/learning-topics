namespace GlobbingApp2.Classes;

public static class LongExtensions
{
    private static readonly string[] SizeSuffixes =
    [
        "B",
        "KB",
        "MB",
        "GB",
        "TB",
        "PB",
        "EB"
    ];

    /// <summary>
    /// Converts a file size, represented in bytes, to a human-readable string format.
    /// </summary>
    /// <param name="bytes">The size in bytes to be converted.</param>
    /// <param name="decimalPlaces">The number of decimal places to include in the formatted output. Defaults to 2.</param>
    /// <returns>A string representing the file size in a human-readable format, including the appropriate size suffix (e.g., B, KB, MB).</returns>
    /// <example>
    /// <code>
    /// long fileSize = 1048576;
    /// string readableSize = fileSize.ToFileSize(); // "1.00 MB"
    /// </code>
    /// </example>
    public static string ToFileSize(this long bytes, int decimalPlaces = 2)
    {
        if (bytes == 0)
        {
            return "0 B";
        }

        var size = Math.Abs((double)bytes);
        var index = 0;

        while (size >= 1024 && index < SizeSuffixes.Length - 1)
        {
            size /= 1024;
            index++;
        }

        size *= Math.Sign(bytes);

        var format = $"N{decimalPlaces}";

        return $"{size.ToString(format)} {SizeSuffixes[index]}";
    }
}