using System.Text.RegularExpressions;

namespace StringsBetweenQuotesExample.Classes;

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