using GlobbingApp1.Models;
using Microsoft.Extensions.FileSystemGlobbing;

namespace GlobbingApp1.Classes;

/// <summary>
/// Provides functionality for matching file system entries using include and exclude patterns.
/// </summary>
/// <remarks>
/// This class utilizes the <see cref="Matcher"/> to perform
/// pattern-based file matching. It is designed to simplify the process of retrieving file paths
/// that match specific criteria within a given directory.
/// </remarks>
public class Globbing
{
    /// <summary>
    /// Asynchronously retrieves a function that, when invoked, returns a list of image file paths
    /// matching the specified include and exclude patterns within a given directory.
    /// </summary>
    /// <param name="mp">
    /// An instance of <see cref="MatcherParameters"/> containing the parent folder path,
    /// include patterns, and exclude patterns for file matching.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation. The task result is a function that,
    /// when called, returns a list of file paths matching the specified criteria.
    /// </returns>
    /// <remarks>
    /// This method uses the <see cref="Matcher"/> class to perform pattern-based file matching.
    /// It simplifies the process of filtering file paths based on include and exclude patterns.
    /// </remarks>
    public static async Task<Func<List<string>>> GetAsync(MatcherParameters mp)
    {
        Matcher matcher = new();
        matcher.AddIncludePatterns(mp.Patterns);
        matcher.AddExcludePatterns(mp.ExcludePatterns);

        return await Task.FromResult(() => matcher.GetResultsInFullPath(mp.ParentFolder)
            .ToList())
            .ConfigureAwait(false);
    }


    /// <summary>
    /// Asynchronously retrieves a list of duplicate file paths within the specified parent folder.
    /// </summary>
    /// <param name="parentFolder">
    /// The path to the parent folder where the search for duplicate files will be performed.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation. The task result is a list of 
    /// <see cref="FileMatchItem"/> objects representing the duplicate files found.
    /// </returns>
    /// <remarks>
    /// This method uses the <see cref="Matcher"/> class to identify duplicate files based on 
    /// predefined include and exclude patterns. The results are saved to a file named "Duplicates.txt".
    /// </remarks>
    public static async Task<List<FileMatchItem>> GetDuplicatesTask(string parentFolder )
    {
        List<FileMatchItem> list = [];
        Matcher matcher = new();
        matcher.AddIncludePatterns([
            "**/* - Copy .*", 
            "**/* - Copy (*.*"
        ]);
        matcher.AddExcludePatterns([
            "**/My Music/**",
            "**/My Pictures/**",
            "**/My Videos/**"
        ]);

        await Task.Run(() =>
        {
            foreach (string file in matcher.GetResultsInFullPath(parentFolder))
            {
                list.Add(new FileMatchItem(file));
            }
        });

        await File.WriteAllLinesAsync($"Duplicates.txt", 
            list.Select(item => item.ToString()));
        
        return list;
    }

}