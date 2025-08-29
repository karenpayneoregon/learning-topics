using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NestedClassesFinder.Classes.Standard;

/// <summary>
/// Provides functionality to generate a `.filenesting.json` file based on mismatches
/// between class names and file names, as identified by <see cref="NestedHelper.Find"/>.
/// </summary>
public static class FileNestingWriter
{
    /// <summary>
    /// Generates a .filenesting.json file from mismatches found by NestedHelper.Find.
    /// </summary>
    public static void CreateFileNestingJson(string rootDirectory, string outputPath = ".filenesting.json")
    {
        var mismatches = NestedHelper.Find(rootDirectory);

        // Dictionary: child file → parent file(s)
        var fileToFile = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

        foreach (var (className, filePath) in mismatches)
        {
            var childFile = Path.GetFileName(filePath);
            var parentFile = className + ".cs";

            if (!fileToFile.ContainsKey(childFile))
                fileToFile[childFile] = new List<string>();

            if (!fileToFile[childFile].Contains(parentFile, StringComparer.OrdinalIgnoreCase))
                fileToFile[childFile].Add(parentFile);
        }

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

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize(jsonObject, options);
        //File.WriteAllText(Path.Combine(rootDirectory, outputPath), json);
        File.WriteAllText(outputPath, json);
    }
}

