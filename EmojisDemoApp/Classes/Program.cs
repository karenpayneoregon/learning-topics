using ConsoleConfigurationLibrary.Classes;
using ConsoleHelperLibrary.Classes;
using EmojisDemoApp.Classes;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using static ConsoleConfigurationLibrary.Classes.ApplicationConfiguration;

// ReSharper disable once CheckNamespace
namespace EmojisDemoApp;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        var assembly = Assembly.GetEntryAssembly();
        var product = assembly?.GetCustomAttribute<AssemblyProductAttribute>()?.Product;

        Console.Title = product!;

        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);

        SetupLogging.Development();

        Console.OutputEncoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
        Console.InputEncoding = Encoding.UTF8;

    }

}
