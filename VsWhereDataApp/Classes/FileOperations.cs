using System.Diagnostics;
using System.Text.Json;
using VsWhereDataApp.Models;

namespace VsWhereDataApp.Classes;
internal class FileOperations
{
    /// <summary>
    /// Executes the vswhere.exe utility to retrieve information about installed Visual Studio instances
    /// and writes the output in JSON format to the specified file path.
    /// </summary>
    /// <param name="outputPath">
    /// The file path where the JSON output from vswhere.exe will be saved. If the file already exists,
    /// it will be overwritten.
    /// </param>
    /// <exception cref="FileNotFoundException">
    /// Thrown when the vswhere.exe executable cannot be found in the expected locations or on the system PATH.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the vswhere.exe process fails to start, returns a non-zero exit code, or produces no output.
    /// </exception>
    /// <remarks>
    /// This method uses vswhere.exe with the following arguments:
    /// - <c>-format json</c>: Outputs the data in JSON format.
    /// - <c>-prerelease</c>: Includes prerelease versions of Visual Studio.
    /// - <c>-products *</c>: Includes all product types (e.g., Community, Professional, Enterprise, BuildTools).
    /// The caller is responsible for ensuring that the specified output path is accessible and writable.
    /// </remarks>
    public static void GenerateDataJson(string outputPath)
    {
        string vswhere = ResolveVsWherePath();
        
        if (!File.Exists(vswhere))
            throw new FileNotFoundException("vswhere.exe not found. Install Visual Studio Installer or place vswhere.exe on PATH.", vswhere);

        // -format json: output JSON
        // -prerelease: include prerelease if any
        // -products *: include all product types (Community/Pro/Enterprise/BuildTools, etc.)
        var psi = new ProcessStartInfo
        {
            FileName = vswhere,
            Arguments = "-format json -prerelease -products *",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        using var p = Process.Start(psi);
        if (p == null) throw new InvalidOperationException("Failed to start vswhere.exe.");
        
        string stdout = p.StandardOutput.ReadToEnd();
        string stderr = p.StandardError.ReadToEnd();
        p.WaitForExit();

        if (p.ExitCode != 0)
            throw new InvalidOperationException($"vswhere.exe failed with exit code {p.ExitCode}: {stderr}");

        if (string.IsNullOrWhiteSpace(stdout))
            throw new InvalidOperationException("vswhere.exe returned no data.");

        File.WriteAllText(outputPath, stdout);
    }


    /// <summary>
    /// Reads a JSON file containing installation data and deserializes it into a list of <see cref="Installation"/> objects.
    /// </summary>
    /// <param name="path">The file path to the JSON file.</param>
    /// <returns>A list of <see cref="Installation"/> objects deserialized from the JSON file.</returns>
    /// <exception cref="FileNotFoundException">Thrown when the specified file does not exist.</exception>
    public static List<Installation> ReadDataJson(string path)
    {
        if (!File.Exists(path)) throw new FileNotFoundException("vs.json not found.", path);
        
        var list = JsonSerializer.Deserialize<List<Installation>>(File.ReadAllText(path), Options) ?? [];
        
        File.Delete(path);
        
        return list;
    }

    /// <summary>
    /// Resolves the path to the vswhere.exe executable by checking common installation locations
    /// and falling back to the system PATH if necessary.
    /// </summary>
    /// <returns>
    /// The full path to the vswhere.exe executable. If the executable is not found in the common
    /// locations or on the PATH, the default candidate path is returned.
    /// </returns>
    /// <remarks>
    /// This method attempts to locate vswhere.exe in the standard installation directory used by
    /// the Visual Studio Installer. If it is not found there, it tries to resolve the path using
    /// the system PATH environment variable. The caller is responsible for verifying the existence
    /// of the returned path.
    /// </remarks>
    private static string ResolveVsWherePath()
    {
        // Standard install path with Visual Studio Installer
        string pf86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
        var candidates = new[]
        {
            Path.Combine(pf86, "Microsoft Visual Studio", "Installer", "vswhere.exe"),
            "vswhere.exe" // rely on PATH
        };

        foreach (var candidate in candidates)
        {
            try
            {
                string full = candidate;
                if (!Path.IsPathRooted(candidate))
                {
                    // try to resolve from PATH
                    var which = new ProcessStartInfo
                    {
                        FileName = Environment.OSVersion.Platform == PlatformID.Win32NT ? "where" : "which",
                        Arguments = "vswhere.exe",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    };
                    
                    using var proc = Process.Start(which);
                    string pathOut = proc?.StandardOutput.ReadLine() ?? "";
                    proc?.WaitForExit();
                    
                    if (!string.IsNullOrWhiteSpace(pathOut) && File.Exists(pathOut)) return pathOut;
                }
                else if (File.Exists(full))
                {
                    return full;
                }
            }
            catch
            {
                 /* ignore and try next */
            }
        }
        
        return candidates[0]; // return default; caller will verify existence
    }

    public static JsonSerializerOptions Options => new()
    {
        PropertyNameCaseInsensitive = false,
        AllowTrailingCommas = true,
        ReadCommentHandling = JsonCommentHandling.Skip
    };
}


