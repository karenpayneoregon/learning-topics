using System.Text.RegularExpressions;

namespace StringsBetweenQuotesExample.Classes;

/// <summary>
/// Represents a utility class for counting the occurrences of HTML &lt;table&gt; tags 
/// within files located in a specified root directory.
/// </summary>
/// <remarks>
/// This class is designed to recursively search through all files with a specific 
/// extension in the given directory and count the number of &lt;table&gt; tags using 
/// regular expressions.
/// </remarks>
public partial class HtmlTableCounter
{
    public string RootDirectory { get; }

    public HtmlTableCounter(string rootDirectory)
    {
        if (string.IsNullOrWhiteSpace(rootDirectory) || !Directory.Exists(rootDirectory))
        {
            throw new ArgumentException("Provided directory path is invalid.", nameof(rootDirectory));
        }

        RootDirectory = rootDirectory;
    }

    public int CountTableTags() 
        => Directory.EnumerateFiles(RootDirectory, "*.cfm", SearchOption.AllDirectories)
            .Select(File.ReadAllText).Select(content => HtmlTableRegex().Matches(content).Count)
            .Sum();

    [GeneratedRegex(@"<table\b", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex HtmlTableRegex();
}