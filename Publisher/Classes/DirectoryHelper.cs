using Publisher.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Publisher.Classes;

public static class DirectoryHelper
{
    /// <summary>
    /// Removes all whitespace characters from the specified string.
    /// </summary>
    /// <param name="input">The input string from which whitespace characters will be removed.</param>
    /// <returns>A new string with all whitespace characters removed.</returns>
    public static string RemoveWhitespace(this string input) => 
        new(input.ToCharArray().Where(c => !char.IsWhiteSpace(c)).ToArray());

    /// <summary>
    /// Given a folder name return all parents according to level
    /// </summary>
    /// <param name="folderName">Sub-folder name</param>
    /// <param name="level">Level to move up the folder chain</param>
    /// <returns>List of folders dependent on level parameter</returns>
    public static string UpperFolder(this string folderName, int level)
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
            folderList.Add(folderName);
        }

        if (folderList.Count > 0 && level > 0)
        {
            if (level - 1 <= folderList.Count - 1)
            {
                return folderList[level - 1];
            }
            else
            {
                return folderName;
            }
        }
        else
        {
            return folderName;
        }
    }
    /// <summary>
    /// Given a path for a project obtains it's solution folder
    /// </summary>
    /// <returns>Current solution folder from project folder</returns>
    public static string CurrentSolutionFolder() => AppDomain.CurrentDomain.BaseDirectory.UpperFolder(5);
    public static string SolutionName() => Directory.GetFiles(CurrentSolutionFolder(), "*.sln").FirstOrDefault();
    /// <summary>
    /// Retrieves the name of the first project file with the specified extension in the given folder.
    /// </summary>
    /// <param name="folder">The folder to search for the project file.</param>
    /// <param name="extension">
    /// The file extension of the project file to search for (default is "csproj").
    /// </param>
    /// <returns>
    /// The full path of the first project file found with the specified extension in the folder,
    /// or <c>null</c> if no such file is found.
    /// </returns>
    public static string ProjectName(string folder, string extension = "csproj") => 
        Directory.GetFiles(folder, $"*.{extension}").FirstOrDefault();

    public static List<ProjectItem> ProjectItems(List<string> projectNamesList)
    {
        List<ProjectItem> items = [];
        var projects = ApplicationSettings.Instance.ClassProjectsList;
        
        foreach (var name in projects)
        {
            var path = Path.Combine(name.Path, "Bin", "Debug");

            if (!Directory.Exists(path)) continue;
            
            var packNames = Directory.GetFiles(path, "*.nupkg");
            
            if (packNames.Length <= 0) continue;
            
            var item = new ProjectItem {Name = name.Path};
                        
            foreach (var packName in packNames)
            {
                var version = Path.GetFileNameWithoutExtension(packName);
                //version = version.Substring( version.IndexOf(".", StringComparison.Ordinal) +1);

                var parts = version.Split('.');
                version = string.Join('.', parts.SkipWhile(p => !char.IsDigit(p[0])));
                
                item.PackageList.Add(new Package()
                {
                    Name = Path.GetFileName(packName), 
                    Version = new Version(version)
                });
                            
                item.Path = Path.GetDirectoryName(packName);
            }

            items.Add(item);
        }

        return items;

    }
}