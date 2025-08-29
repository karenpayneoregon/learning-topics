using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace NestedClassesFinder.Classes;


public static class NestedHelper
{
    /// <summary>
    /// Finds partial classes whose file name does NOT match the declared class name.
    /// Skips generated files, Program.cs, and intentional suffixes like .Designer.cs.
    /// Returns (ClassName, FilePath).
    /// </summary>
    public static IEnumerable<(string ClassName, string FilePath)> Find(string rootDirectory)
    {
        if (string.IsNullOrWhiteSpace(rootDirectory))
            throw new ArgumentException("Root directory is required.", nameof(rootDirectory));

        if (!Directory.Exists(rootDirectory))
            throw new DirectoryNotFoundException($"Directory not found: {rootDirectory}");

        var partialClassPattern =
            new Regex(@"\bpartial\s+(?:record\s+)?class\s+([A-Za-z_][A-Za-z0-9_]*)\b",
                      RegexOptions.Compiled | RegexOptions.Multiline);

        string[] ignoreNameSuffixes = new[]
        {
            ".Designer", 
            ".g", ".g.i", 
            ".razor", 
            ".razor.g", 
            ".AssemblyInfo", 
            ".GlobalUsings"
        };

        bool HasIgnoredSuffix(string fileBaseName)
            => ignoreNameSuffixes.Any(sfx => fileBaseName.EndsWith(sfx, StringComparison.OrdinalIgnoreCase));

        var mismatches = new List<(string ClassName, string FilePath)>();

        foreach (var file in EnumerateCsFiles(rootDirectory))
        {
            var baseName = Path.GetFileNameWithoutExtension(file);

            // skip Program.cs outright
            if (baseName.Equals("Program", StringComparison.OrdinalIgnoreCase))
                continue;

            var isIgnoredFileName = HasIgnoredSuffix(baseName);

            string text;
            try { text = File.ReadAllText(file); }
            catch { continue; }

            foreach (Match m in partialClassPattern.Matches(text))
            {
                var className = m.Groups[1].Value;
                var nameMatches = baseName.Equals(className, StringComparison.Ordinal);

                if (!nameMatches && !isIgnoredFileName)
                {
                    mismatches.Add((className, file));
                }
            }
        }

        return mismatches;

    }

    /// <summary>
    /// Recursively enumerates all C# files (*.cs) in the specified directory and its subdirectories.
    /// Skips directories such as "bin", "obj", ".git", and ".vs".
    /// </summary>
    /// <param name="root">The root directory to start searching for C# files.</param>
    /// <returns>An enumerable collection of file paths to C# files.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="root"/> is <c>null</c>.</exception>
    /// <exception cref="DirectoryNotFoundException">Thrown if the specified <paramref name="root"/> directory does not exist.</exception>
    private static IEnumerable<string> EnumerateCsFiles(string root)
    {
        var stack = new Stack<string>();
        stack.Push(root);

        while (stack.Count > 0)
        {
            var current = stack.Pop();
            if (ShouldSkipDir(current))
                continue;

            string[] subDirs;
            try { subDirs = Directory.GetDirectories(current); }
            catch { continue; }

            foreach (var dir in subDirs)
                stack.Push(dir);

            string[] files;
            try { files = Directory.GetFiles(current, "*.cs"); }
            catch { continue; }

            foreach (var file in files)
                yield return file;
        }
    }

    private static bool ShouldSkipDir(string path)
    {
        var dir = Path.GetFileName(path.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
        return dir.Equals("bin", StringComparison.OrdinalIgnoreCase)
            || dir.Equals("obj", StringComparison.OrdinalIgnoreCase)
            || dir.Equals(".git", StringComparison.OrdinalIgnoreCase)
            || dir.Equals(".vs", StringComparison.OrdinalIgnoreCase);
    }
}
