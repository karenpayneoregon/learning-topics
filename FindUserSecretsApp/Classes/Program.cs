using FindUserSecretsApp.Classes;
using System.Reflection;
using System.Runtime.CompilerServices;
using SecretsLibrary.Classes;
using SerilogLibrary;

// ReSharper disable once CheckNamespace
namespace FindUserSecretsApp;
internal partial class Program
{
    /// <summary>
    /// Initializes the application by setting the console title, positioning the console window, 
    /// and configuring logging for development purposes.
    ///
    /// Also sets title from the assembly product attribute read from the project file.
    /// </summary>

    [ModuleInitializer]
    public static void Init()
    {
        var assembly = Assembly.GetEntryAssembly();
        var product = assembly?.GetCustomAttribute<AssemblyProductAttribute>()?.Product;

        Console.Title = product!;

        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
        SetupLogging.Development();
    }

}