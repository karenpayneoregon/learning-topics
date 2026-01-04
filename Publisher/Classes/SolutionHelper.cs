using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Publisher.Classes;

public partial class SolutionHelper
{
    /// <summary>
    /// Returns project names from a solution file
    /// </summary>
    /// <param name="solutionName"></param>
    /// <returns></returns>
    public static List<string> ProjectNames(string solutionName)
    {

        var projectList = new List<string>();

        var content = File.ReadAllText(solutionName);
        var projReg = ProjectRegex();

        var matches = projReg.Matches(content).Cast<Match>();

        var projects = matches.Select(x => x.Groups[2].Value).ToList();
            
        for (var index = 0; index < projects.Count; ++index)
        {
            if (!Path.IsPathRooted(projects[index]))
            {
                projects[index] = Path.Combine(path1: Path.GetDirectoryName(solutionName)!, path2: projects[index]);
            }

            projects[index] = Path.GetFullPath(projects[index]);
            projectList.Add(Path.GetFileNameWithoutExtension(projects[index]));
        }

        return projectList;
    }

    [GeneratedRegex("Project\\(\"\\{[\\w-]*\\}\"\\) = \"([\\w _]*.*)\", \"(.*\\.(cs|vcx|vb)proj)\"", RegexOptions.Compiled)]
    private static partial Regex ProjectRegex();
}