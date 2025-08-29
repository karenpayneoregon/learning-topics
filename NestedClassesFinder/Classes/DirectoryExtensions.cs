using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedClassesFinder.Classes;
public static class DirectoryExtensions
{
    /// <summary>
    /// Given a folder name return all parents according to level
    /// </summary>
    /// <param name="pFolderName">Sub-folder name</param>
    /// <param name="level">Level to move up the folder chain</param>
    /// <returns>A physical folder path</returns>
    public static string UpperFolder(this string pFolderName, decimal level)
    {
        return UpperFolder(pFolderName, Convert.ToInt32(level));
    }
    /// <summary>
    /// Given a folder name return all parents according to level
    /// </summary>
    /// <param name="pFolderName">Sub-folder name</param>
    /// <param name="pLevel">Level to move up the folder chain</param>
    /// <returns>List of folders dependent on level parameter</returns>
    public static string UpperFolder(this string pFolderName, int pLevel)
    {
        var folderList = new List<string>();

        while (!string.IsNullOrWhiteSpace(pFolderName))
        {
            var parentFolder = Directory.GetParent(pFolderName);
            if (parentFolder == null)
            {
                break;
            }

            pFolderName = Directory.GetParent(pFolderName).FullName;
            folderList.Add(pFolderName);
        }

        if (folderList.Count > 0 && pLevel > 0)
        {
            return pLevel - 1 <= folderList.Count - 1 ? folderList[pLevel - 1] : pFolderName;
        }
        else
        {
            return pFolderName;
        }
    }
    public static string CurrentProjectFolder(this string sender)
    {
        return sender.UpperFolder(3);
    }
    /// <summary>
    /// Get a list of all folders above 'FolderName'
    /// </summary>
    /// <param name="pFolderName">Folder to start at</param>
    /// <param name="pSort">True/False</param>
    /// <returns>List of folder names</returns>
    public static List<string> UpperFolderList(this string pFolderName, bool pSort)
    {
        var folderList = new List<string>();

        while (!string.IsNullOrWhiteSpace(pFolderName))
        {
            var parentFolder = Directory.GetParent(pFolderName);
            if (parentFolder == null)
            {
                break;
            }
            pFolderName = Directory.GetParent(pFolderName).FullName;
            folderList.Add(pFolderName);
        }

        if (pSort)
        {
            folderList.Sort();
        }

        return folderList;
    }
}
