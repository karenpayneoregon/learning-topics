using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace CommonHelpersLibrary;
/// <summary>
/// Provides utility methods to retrieve assembly metadata such as company, product, copyright, and version information.
/// </summary>
public class Info
{
    /// <summary>
    /// Represents details about the caller of a method, including assembly information,
    /// target framework, type and method names, file path, and line number.
    /// </summary>
    public readonly record struct CallerDetails(string? AssemblyName, string? AssemblyVersion,
        string? TargetFramework, string? TypeName, string? MethodName, string? FilePath, int LineNumber);

    // Build caller details. Use Caller Info attrs for member/file/line;
    // use GetCallingAssembly for the assembly; use StackTrace for type.
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static CallerDetails BuildCallerDetails(string? memberName, string? filePath, int lineNumber)
    {
        var callingAsm = Assembly.GetCallingAssembly();
        string? typeName = null;

        try
        {
            // Skip this helper frame; capture the immediate external frame.
            var st = new StackTrace(skipFrames: 1, fNeedFileInfo: false);
            var frame = st.GetFrame(0);
            typeName = frame?.GetMethod()?.DeclaringType?.FullName;
        }
        catch
        {
            // Best-effort; leave typeName null if anything goes sideways.
        }

        var asmName = callingAsm.GetName();
        var framework = callingAsm.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName;

        return new CallerDetails(
            AssemblyName: asmName?.Name,
            AssemblyVersion: asmName?.Version?.ToString(),
            TargetFramework: framework,
            TypeName: typeName,
            MethodName: memberName,
            FilePath: filePath,
            LineNumber: lineNumber);
    }


    /// <summary>
    /// Retrieves the copyright information associated with the calling assembly.
    /// </summary>
    /// <returns>
    /// A <see cref="string"/> containing the copyright information, or "No copyright information found" 
    /// if the copyright is not specified in the assembly metadata.
    /// </returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string GetCopyright()
    {
        var asm = Assembly.GetCallingAssembly();
        var attr = asm.GetCustomAttribute<AssemblyCopyrightAttribute>();
        return attr?.Copyright ?? "No copyright information found.";
    }

    /// <summary>
    /// Retrieves the copyright information associated with the calling assembly.
    /// </summary>
    /// <param name="caller">
    /// When this method returns, contains details about the caller, including the assembly name, 
    /// version, target framework, type name, method name, file path, and line number.
    /// </param>
    /// <param name="memberName">
    /// The name of the member that invoked this method. This parameter is optional and is automatically 
    /// populated by the compiler if not explicitly provided.
    /// </param>
    /// <param name="filePath">
    /// The file path of the source code that invoked this method. This parameter is optional and is 
    /// automatically populated by the compiler if not explicitly provided.
    /// </param>
    /// <param name="lineNumber">
    /// The line number in the source code file that invoked this method. This parameter is optional 
    /// and is automatically populated by the compiler if not explicitly provided.
    /// </param>
    /// <returns>
    /// A <see cref="string"/> containing the copyright information, or "No copyright information found" 
    /// if the copyright is not specified in the assembly metadata.
    /// </returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string GetCopyright(out CallerDetails caller, [CallerMemberName] string? memberName = null, [CallerFilePath] string? filePath = null, [CallerLineNumber] int lineNumber = 0)
    {
        caller = BuildCallerDetails(memberName, filePath, lineNumber);
        return GetCopyright();
    }

    /// <summary>
    /// Retrieves the company name associated with the calling assembly.
    /// </summary>
    /// <returns>
    /// A <see cref="string"/> containing the company name, or "No company information found" 
    /// if the company name is not specified in the assembly metadata.
    /// </returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string GetCompany()
    {
        var asm = Assembly.GetCallingAssembly();
        var attr = asm.GetCustomAttribute<AssemblyCompanyAttribute>();
        return attr?.Company ?? "No company information found.";
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string GetCompany(
        out CallerDetails caller,
        [CallerMemberName] string? memberName = null,
        [CallerFilePath] string? filePath = null,
        [CallerLineNumber] int lineNumber = 0)
    {
        caller = BuildCallerDetails(memberName, filePath, lineNumber);
        return GetCompany();
    }

    // -----------------------
    // PRODUCT
    // -----------------------

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string GetProduct()
    {
        var asm = Assembly.GetCallingAssembly();
        var attr = asm.GetCustomAttribute<AssemblyProductAttribute>();
        return attr?.Product ?? "No product information found.";
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string GetProduct(
        out CallerDetails caller,
        [CallerMemberName] string? memberName = null,
        [CallerFilePath] string? filePath = null,
        [CallerLineNumber] int lineNumber = 0)
    {
        caller = BuildCallerDetails(memberName, filePath, lineNumber);
        return GetProduct();
    }

    // -----------------------
    // VERSION
    // -----------------------

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Version GetVersion()
    {
        var asm = Assembly.GetCallingAssembly();
        return asm.GetName().Version ?? new Version(1, 0, 0, 0);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Version GetVersion(
        out CallerDetails caller,
        [CallerMemberName] string? memberName = null,
        [CallerFilePath] string? filePath = null,
        [CallerLineNumber] int lineNumber = 0)
    {
        caller = BuildCallerDetails(memberName, filePath, lineNumber);
        return GetVersion();
    }
}
