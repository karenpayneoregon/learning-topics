using System.Diagnostics;

namespace TempApp.Classes;
internal static class Work
{
    [DebuggerStepThrough, DebuggerStepperBoundary]
    public static void Run()
    {
        Method2();
    }

    private static void Method2()
    {
        Console.WriteLine("Skipped but still executes");
    }
}
