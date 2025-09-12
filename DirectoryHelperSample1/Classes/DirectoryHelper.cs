namespace DirectoryHelperSample1.Classes;

public static class DirectoryHelper
{
    /// <summary>
    /// Retrieves the specified folder and all its ancestor folders in a dictionary format.
    /// The dictionary keys represent the depth level, where 0 corresponds to the starting (deepest) folder,
    /// and the keys increase as the folders approach the root directory.
    /// The dictionary is populated in descending key order, enabling enumeration from the root to the deepest folder.
    /// </summary>
    /// <param name="folderName">
    /// The full path of the starting folder. If <c>null</c> or empty, an empty dictionary is returned.
    /// </param>
    /// <returns>
    /// A dictionary where the keys are integers representing the depth level (0 for the deepest folder),
    /// and the values are the full paths of the folders.
    /// </returns>
    public static Dictionary<int, string> UpperFolders(this string? folderName)
    {
        var result = new Dictionary<int, string>();
        
        if (string.IsNullOrWhiteSpace(folderName))
            return result;

        var directoryInfo = new DirectoryInfo(folderName);
        var chain = new List<string>();

        // chain[0] = deepest (starting) folder, last = root
        while (directoryInfo is not null)
        {
            chain.Add(directoryInfo.FullName);
            directoryInfo = directoryInfo.Parent;
        }

        // Keys: 0…N where 0 = deepest; insert high->low 
        for (var index = chain.Count - 1; index >= 0; index--)
        {
            result[index] = chain[index];
        }

        return result;
    }
}