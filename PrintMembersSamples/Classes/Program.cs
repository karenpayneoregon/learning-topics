using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace PrintMembersSamples;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        Console.Title = System.Diagnostics.Debugger.IsAttached ? "PrintMembers Full" : "PrintMembers redacted";

        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
    }

}
