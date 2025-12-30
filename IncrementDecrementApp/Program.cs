using IncrementDecrementApp.Classes;
using IncrementDecrementApp.Classes.System;

namespace IncrementDecrementApp;
internal partial class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(12.Increment(5));
        Console.WriteLine(12.Decrement(2));

        Console.WriteLine(12.5m.Increment(2.5m));
        Console.WriteLine(12.5m.Decrement(4.5m));
        
        SpectreConsoleHelpers.ExitPrompt();
        
    }
}

