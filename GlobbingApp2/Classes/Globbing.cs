using Microsoft.Extensions.FileSystemGlobbing;

namespace GlobbingApp2.Classes;


public class Globbing
{

    /// <summary>
    /// Asynchronously retrieves a list of Word document files from the user's "My Documents" folder.
    /// </summary>
    /// <remarks>
    /// The method uses file system globbing to include files with extensions <c>.docx</c> and <c>.doc</c>, 
    /// while excluding files located in "My Music", "My Pictures", and "My Videos" subdirectories.
    /// </remarks>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a list of 
    /// <see cref="FileMatchItem"/> objects representing the matched Word document files.
    /// </returns>
    /// <exception cref="System.IO.IOException">
    /// Thrown if an I/O error occurs while accessing the file system.
    /// </exception>
    /// <example>
    /// <code>
    /// List&lt;FileMatchItem&gt; wordDocuments = await Globbing.GetWordDocumentsTask();
    /// foreach (var document in wordDocuments)
    /// {
    ///     Console.WriteLine(document.FullName);
    /// }
    /// </code>
    /// </example>
    public static async Task<List<FileMatchItem>> GetWordDocumentsTask()
    {

        string parentFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        List<FileMatchItem> list = [];
        Matcher matcher = new();
        
        matcher.AddIncludePatterns(["**/*.docx", "**/*.doc"]);

        matcher.AddExcludePatterns([
            "**/My Music/**",
            "**/My Pictures/**",
            "**/My Videos/**"
        ]);

        await Task.Run(() =>
        {
            foreach (var file in matcher.GetResultsInFullPath(parentFolder))
            {
                // modify to get file size and add it to the FileMatchItem class
                FileInfo fileInfo = new FileInfo(file);
                list.Add(new FileMatchItem(file) { Size = fileInfo.Length });   
            }
        });

        return list;

    }
}

