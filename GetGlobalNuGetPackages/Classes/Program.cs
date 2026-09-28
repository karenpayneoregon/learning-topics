using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace GetGlobalNuGetPackages;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        Console.Title = "NuGet Packages";
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
    }
}
