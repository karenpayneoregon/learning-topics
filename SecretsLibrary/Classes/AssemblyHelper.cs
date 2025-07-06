using System.Diagnostics;
using System.Reflection;

namespace SecretsLibrary.Classes;
public class AssemblyHelper
{

    /// <summary>
    /// Retrieves the namespace of the calling assembly that references the current assembly.
    /// </summary>
    /// <returns>
    /// The namespace of the last calling assembly that references the current assembly, 
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
    /// The name of the last calling assembly (without its file extension) that references the current assembly, 
    /// or <c>null</c> if no such assembly is found.
    /// </returns>
    public static string CallingAssemblyName() => Path.GetFileNameWithoutExtension(CallerAssembly()?.Location);
}
