using ConsoleConfigurationLibrary.Classes;
using PromptFilesExamplesApp.Classes;
using System.Reflection;
using System.Runtime.CompilerServices;
using PromptFilesExamplesApp.Models;
using static PromptFilesExamplesApp.Classes.SpectreConsoleHelpers;

// ReSharper disable once CheckNamespace
namespace PromptFilesExamplesApp;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        var assembly = Assembly.GetEntryAssembly();
        var product = assembly?.GetCustomAttribute<AssemblyProductAttribute>()?.Product;

        Console.Title = product!;
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
    }
}
