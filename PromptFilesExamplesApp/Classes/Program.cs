using System.Reflection;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace PromptFilesExamplesApp;
internal partial class Program
{
    /// <summary>
    /// Initializes the program by setting the console window title and positioning the console window.
    /// </summary>
    /// <remarks>
    /// This method is marked with the <see cref="ModuleInitializerAttribute"/> to ensure it is executed 
    /// automatically before the program's entry point.
    /// </remarks>
    [ModuleInitializer]
    public static void Init()
    {
        var assembly = Assembly.GetEntryAssembly();
        var product = assembly?.GetCustomAttribute<AssemblyProductAttribute>()?.Product;

        Console.Title = product!;
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
    }
}
