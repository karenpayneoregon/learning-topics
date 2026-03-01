using SolutionFrameworkScanner.Models;
using System.Xml.Linq;

namespace SolutionFrameworkScanner.Classes;

/// <summary>
/// Provides functionality to analyze and retrieve framework information
/// for all projects within a specified solution file.
/// </summary>
/// <remarks>
/// This class includes methods to parse solution files, extract project paths,
/// and determine the frameworks targeted by each project. It is designed to
/// facilitate framework analysis for .NET solutions.
/// </remarks>
public static class Scanner
{
    /// <summary>
    /// Reads the specified solution file and retrieves framework information
    /// for all projects contained within it.
    /// </summary>
    /// <param name="solutionPath">
    /// The full path to the solution file to be analyzed.
    /// </param>
    /// <returns>
    /// A list of <see cref="ProjectFrameworkInfo"/> objects, each representing
    /// a project within the solution and its associated target frameworks.
    /// </returns>
    /// <exception cref="FileNotFoundException">
    /// Thrown when the specified solution file does not exist.
    /// </exception>
    /// <remarks>
    /// This method parses the solution file to extract project paths and then
    /// determines the frameworks targeted by each project. It is useful for
    /// analyzing .NET solutions to understand their framework dependencies.
    /// </remarks>
    public static List<ProjectFrameworkInfo> ReadSolution(string solutionPath)
    {
        if (!File.Exists(solutionPath))
            throw new FileNotFoundException("Solution not found.", solutionPath);

        var slnDir = Path.GetDirectoryName(Path.GetFullPath(solutionPath))!;
        var projectPaths = GetProjectPathsFromSln(solutionPath, slnDir);

        var results = new List<ProjectFrameworkInfo>();

        foreach (var projPath in projectPaths)
        {
            var info = ReadProjectFrameworks(projPath);
            results.Add(info);
        }

        return results;
    }

    /// <summary>
    /// Extracts the paths of all MSBuild-style projects (e.g., .csproj, .vbproj, .fsproj)
    /// from the specified solution file.
    /// </summary>
    /// <param name="slnPath">
    /// The full path to the solution file to be parsed.
    /// </param>
    /// <param name="slnDir">
    /// The directory containing the solution file, used to resolve relative project paths.
    /// </param>
    /// <returns>
    /// A list of full paths to the MSBuild-style projects found in the solution file.
    /// </returns>
    /// <remarks>
    /// This method performs a simple parsing of the solution file to locate project entries
    /// and resolves their paths relative to the solution directory. Only valid MSBuild-style
    /// project files are included in the result.
    /// </remarks>
    private static List<string> GetProjectPathsFromSln(string slnPath, string slnDir)
    {
        // Very simple .sln parsing: finds lines like:
        // Project("{GUID}") = "Name", "path\to\proj.csproj", "{GUID}"
        var paths = new List<string>();

        foreach (var line in File.ReadLines(slnPath))
        {
            if (!line.StartsWith("Project(", StringComparison.OrdinalIgnoreCase))
                continue;

            var parts = line.Split(',');
            if (parts.Length < 2)
                continue;

            var rel = parts[1].Trim().Trim('"');

            // only MSBuild-style projects
            if (!rel.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase) &&
                !rel.EndsWith(".vbproj", StringComparison.OrdinalIgnoreCase) &&
                !rel.EndsWith(".fsproj", StringComparison.OrdinalIgnoreCase))
                continue;

            var full = Path.GetFullPath(Path.Combine(slnDir, rel));
            if (File.Exists(full))
                paths.Add(full);
        }

        return paths.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
    }

    /// <summary>
    /// Reads and extracts framework information from a specified project file.
    /// </summary>
    /// <param name="projectPath">The absolute path to the project file to be analyzed.</param>
    /// <returns>
    /// A <see cref="ProjectFrameworkInfo"/> object containing details about the project's name, path,
    /// and the frameworks it targets.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the project file contains invalid XML or lacks a root element.
    /// </exception>
    /// <exception cref="FileNotFoundException">
    /// Thrown if the specified project file does not exist.
    /// </exception>
    /// <remarks>
    /// This method parses the project file to identify the target framework(s) specified within.
    /// It supports both single and multiple target frameworks as defined in the project file.
    /// </remarks>
    private static ProjectFrameworkInfo ReadProjectFrameworks(string projectPath)
    {
        var doc = XDocument.Load(projectPath, LoadOptions.PreserveWhitespace);

        // SDK-style csproj usually has no XML namespace; handle both anyway.
        var root = doc.Root ?? throw new InvalidOperationException($"Invalid project XML: {projectPath}");
        XNamespace ns = root.Name.Namespace;

        // Prefer explicit TargetFramework(s) anywhere in PropertyGroup
        var tf = root.Descendants(ns + "TargetFramework").Select(e => (string?)e).FirstOrDefault(v => !string.IsNullOrWhiteSpace(v))?.Trim();
        var tfsRaw = root.Descendants(ns + "TargetFrameworks").Select(e => (string?)e).FirstOrDefault(v => !string.IsNullOrWhiteSpace(v))?.Trim();

        var tfs = SplitFrameworks(tfsRaw);

        return new ProjectFrameworkInfo
        {
            ProjectName = Path.GetFileNameWithoutExtension(projectPath),
            ProjectPath = projectPath,
            TargetFramework = tf,
            TargetFrameworks = tfs
        };
    }

    /// <summary>
    /// Splits a semicolon-separated string of target frameworks into a distinct, trimmed list of frameworks.
    /// </summary>
    /// <param name="targetFrameworks">
    /// A semicolon-separated string containing one or more target frameworks.
    /// </param>
    /// <returns>
    /// A read-only list of distinct, trimmed target frameworks. If the input is null, empty, or whitespace, 
    /// an empty list is returned.
    /// </returns>
    private static IReadOnlyList<string> SplitFrameworks(string? targetFrameworks)
    {
        if (string.IsNullOrWhiteSpace(targetFrameworks))
            return [];

        return targetFrameworks
            .Split([';'], StringSplitOptions.RemoveEmptyEntries)
            .Select(s => s.Trim())
            .Where(s => s.Length > 0)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();
    }
}