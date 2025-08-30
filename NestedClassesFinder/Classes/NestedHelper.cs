using System.Text.RegularExpressions;

namespace NestedClassesFinder.Classes;


public static partial class NestedHelper
{
    // Use your existing Find(...) unchanged if you still need mismatch-only elsewhere.

    /// <summary>
    /// Returns all partial class declarations (ClassName, FilePath, IsStatic).
    /// Skips Program.cs and common generated/aux files.
    /// </summary>
    public static IEnumerable<(string ClassName, string FilePath)> Find(string rootDirectory)
    {
        if (string.IsNullOrWhiteSpace(rootDirectory))
            throw new ArgumentException("Root directory is required.", nameof(rootDirectory));
        if (!Directory.Exists(rootDirectory))
            throw new DirectoryNotFoundException($"Directory not found: {rootDirectory}");

        // Robust regex: matches any "partial ... class <Name>"
        var partialClassPattern = FindPartialClassRegex();

        string[] ignoreNameSuffixes =
        [
            ".Designer", ".g", ".g.i", ".razor", ".razor.g", ".AssemblyInfo", ".GlobalUsings"
        ];

        bool HasIgnoredSuffix(string baseName) =>
            ignoreNameSuffixes.Any(sfx => baseName.EndsWith(sfx, StringComparison.OrdinalIgnoreCase));

        var results = new List<(string ClassName, string FilePath)>();

        foreach (var file in EnumerateCandidateFiles(rootDirectory))
        {
            var baseName = GetCanonicalBaseName(file);

            // Skip Program.cs (normalized name check)
            if (baseName.Equals("Program", StringComparison.OrdinalIgnoreCase))
                continue;

            if (HasIgnoredSuffix(baseName))
                continue;

            string text;
            try { text = File.ReadAllText(file); }
            catch { continue; }

            foreach (Match m in partialClassPattern.Matches(text))
            {
                var className = m.Groups[1].Value;

                // Compare class name vs canonical filename
                var nameMatches = baseName.Equals(className, StringComparison.Ordinal);

                if (!nameMatches)
                    results.Add((className, file));
            }
        }

        return results;
    }

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
            try { subDirs = Directory.GetDirectories(current); } catch { continue; }
            foreach (var d in subDirs) stack.Push(d);

            string[] files;
            try { files = Directory.GetFiles(current, "*.cs"); } catch { continue; }
            foreach (var f in files) yield return f;
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

    private static IEnumerable<string> EnumerateCandidateFiles(string root)
    {
        var stack = new Stack<string>();
        stack.Push(root);

        while (stack.Count > 0)
        {
            var current = stack.Pop();
            if (ShouldSkipDir(current))
                continue;

            string[] subDirs;
            try { subDirs = Directory.GetDirectories(current); } catch { continue; }
            foreach (var d in subDirs) stack.Push(d);

            // IMPORTANT: include everything, not just *.cs
            string[] files;
            try { files = Directory.GetFiles(current, "*"); } catch { continue; }

            foreach (var f in files)
            {
                var name = Path.GetFileName(f);
                // Accept: *.cs, *.cs., *.cs.. (trim trailing dots before checking)
                var trimmed = name.TrimEnd('.');
                if (trimmed.EndsWith(".cs", StringComparison.OrdinalIgnoreCase))
                    yield return f;
            }
        }
    }

    private static string GetCanonicalBaseName(string filePath)
    {
        // Normalize: remove trailing dots, then strip a single .cs extension if present
        var name = Path.GetFileName(filePath).TrimEnd('.'); // e.g., "Customer.Other.cs"
        if (name.EndsWith(".cs", StringComparison.OrdinalIgnoreCase))
            // name = name.Substring(0, name.Length - 3);
            name = name[..^3];       // -> "Customer.Other"
        return name;
    }

    [GeneratedRegex(@"\bpartial\b(?s).*?\bclass\s+([A-Za-z_][A-Za-z0-9_]*)\b", RegexOptions.Compiled)]
    private static partial Regex FindPartialClassRegex();
}