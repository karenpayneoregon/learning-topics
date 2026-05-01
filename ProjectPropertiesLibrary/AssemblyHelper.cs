using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPropertiesLibrary;
public class AssemblyHelper
{

    /// <summary>
    /// Retrieves the assembly of the last caller in the call stack that references the current assembly.
    /// </summary>
    /// <returns>
    /// An <see cref="Assembly"/> object representing the last calling assembly that references the current assembly,
    /// or <c>null</c> if no such assembly is found.
    /// </returns>
    public static Assembly? CallerAssembly()
    {
        var currentAssembly = Assembly.GetExecutingAssembly();

        var callerAssemblies = new StackTrace().GetFrames()?
            .Select(sf => sf?.GetMethod()?.ReflectedType?.Assembly)
            .Where(assembly => assembly != null)
            .Distinct()
            .Where(assembly => assembly!.GetReferencedAssemblies()
                .Any(assemblyName => assemblyName.FullName == currentAssembly.FullName));

        return callerAssemblies?.LastOrDefault();
    }

    /// <summary>
    /// Retrieves the name of the calling assembly that references the current assembly.
    /// </summary>
    /// <returns>
    /// The name of the last calling assembly (excluding its file extension) that references the current assembly, 
    /// or <c>null</c> if no such assembly is found.
    /// </returns>
    public static string? CallingAssemblyName() 
        => CallerAssembly()?.Location is { } location
            ? Path.GetFileNameWithoutExtension(location)
            : null;

    /// <summary>
    /// Retrieves the directory location of the calling assembly that references the current assembly.
    /// </summary>
    /// <returns>
    /// The directory path of the last calling assembly that references the current assembly, 
    /// or <c>null</c> if no such assembly is found.
    /// </returns>
    public static string? CallingAssemblyLocation()
        => CallerAssembly()?.Location is { } location
            ? Path.GetDirectoryName(location)
            : null;
}
