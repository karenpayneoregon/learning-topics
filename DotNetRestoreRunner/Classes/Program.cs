using DotNetRestoreRunner.Classes.Configuration;
using DotNetRestoreRunner.Classes.Core;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace DotNetRestoreRunner;

internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        Console.Title = "DotNetRestoreRunner";
        SpectreConsoleHelpers.SetEncoding();
        SetupLogging.Development();

    }

}
