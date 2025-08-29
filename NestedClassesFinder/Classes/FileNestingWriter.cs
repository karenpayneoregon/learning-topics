using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NestedClassesFinder.Classes;

/// <summary>
/// Provides functionality to generate a `.filenesting.json` file based on mismatches
/// between class names and file names, as identified by <see cref="NestedHelper.Find"/>.
/// </summary>
public static class FileNestingWriter
{
    /// <summary>
    /// Nests the provided child file under EVERY partial class file:
    ///   { "childFileName": [ "ClassA.cs", "ClassB.cs", ... ] }
    /// </summary>
    public static void CreateFileNestingJson_FromPartials(
        string rootDirectory,
        string childFileName,                // e.g., "GeneratedRegularExpressions.cs"
        string outputPath = ".filenesting.json")
    {
        var parents = NestedHelper.Find(rootDirectory)
                                  .Select(p => p.ClassName + ".cs")
                                  .Distinct(StringComparer.OrdinalIgnoreCase)
                                  .OrderBy(s => s, StringComparer.OrdinalIgnoreCase)
                                  .ToList();

        var fileToFile = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
        if (parents.Count > 0)
            fileToFile[childFileName] = parents;

        WriteJson(rootDirectory, outputPath, fileToFile);
    }

    /// <summary>
    /// From mismatch-only results:
    ///   { "ActualFile.cs": [ "ExpectedClassName.cs" ] , ... }
    /// </summary>
    public static void CreateFileNestingJson_FromMismatches(
        string rootDirectory,
        string outputPath = ".filenesting.json")
    {
        var mismatches = NestedHelper.Find(rootDirectory); // IEnumerable<(string ClassName, string FilePath)>

        var fileToFile = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

        foreach (var t in mismatches)
        {
            // If your tuple names are preserved, these work:
            var className = t.ClassName;
            var filePath = t.FilePath;

            // If your compiler doesn't preserve names, uncomment these instead:
            // var className = t.Item1;
            // var filePath  = t.Item2;

            var child = Path.GetFileName(filePath);  // actual file on disk
            var parent = className + ".cs";           // expected name from the class

            if (!fileToFile.TryGetValue(child, out var parents))
            {
                parents = new List<string>();
                fileToFile[child] = parents;
            }

            // Case-insensitive de-dupe
            if (!parents.Any(p => string.Equals(p, parent, StringComparison.OrdinalIgnoreCase)))
                parents.Add(parent);
        }

        // Sort parents for determinism
        foreach (var key in fileToFile.Keys.ToList())
            fileToFile[key] = fileToFile[key].OrderBy(s => s, StringComparer.OrdinalIgnoreCase).ToList();

        var jsonObject = new
        {
            help = "https://go.microsoft.com/fwlink/?linkid=866610",
            root = true,
            dependentFileProviders = new
            {
                add = new
                {
                    fileToFile = new
                    {
                        add = fileToFile
                    }
                }
            }
        };

        var json = JsonSerializer.Serialize(jsonObject, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(".filenesting.json", json);
    }

    private static void WriteJson(string rootDirectory, string outputPath, Dictionary<string, List<string>> fileToFile)
    {
        var jsonObject = new
        {
            help = "https://go.microsoft.com/fwlink/?linkid=866610",
            root = true,
            dependentFileProviders = new
            {
                add = new
                {
                    fileToFile = new
                    {
                        add = fileToFile
                    }
                }
            }
        };

        var json = JsonSerializer.Serialize(jsonObject, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(Path.Combine(rootDirectory, outputPath), json);
    }
}
