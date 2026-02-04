using IComparerIEqualityComparerApp.Classes;
using IComparerIEqualityComparerApp.Classes.Comparers;
using IComparerIEqualityComparerApp.Classes.SystemCode;
using IComparerIEqualityComparerApp.Models;
using Spectre.Console;

namespace IComparerIEqualityComparerApp;
internal partial class Program
{
    static void Main(string[] args)
    {
        Operations.DistinctPeople1();
        Operations.DistinctPeople2();
        Operations.CompareProducts();
        
        SpectreConsoleHelpers.ExitPrompt();
    }


}
