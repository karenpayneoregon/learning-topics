
namespace DirectoryHelperSample2;

public static class DirectoryHelper
{
    public static string? UpperFolder(this string? folderName, int level)
    {
        var folderList = new List<string>();

        while (!string.IsNullOrWhiteSpace(folderName))
        {
            var parentFolder = Directory.GetParent(folderName);
            if (parentFolder == null)
            {
                break;
            }

            folderName = Directory.GetParent(folderName)?.FullName;
            if (!string.IsNullOrWhiteSpace(folderName))
            {
                folderList.Add(folderName);
            }
        }

        return folderList.Count > 0 && level > 0
            ? level - 1 <= folderList.Count - 1 ? folderList[level - 1] : folderName
            : folderName;
    }

    public static string? ProjectFolder() 
        => AppDomain.CurrentDomain.BaseDirectory.UpperFolder(4);

    public static string? ProjectName()
    {
        var projectFolder = ProjectFolder();
        return string.IsNullOrWhiteSpace(projectFolder) ? 
            null :
            Directory.GetFiles(projectFolder, "*.csproj").FirstOrDefault();
    }

    /// <summary>
    /// From project folder, get the solution folder path
    /// </summary>
    /// <returns>folder name</returns>
    public static string SolutionFolder() 
        => AppDomain.CurrentDomain.BaseDirectory.UpperFolder(5)!;
}