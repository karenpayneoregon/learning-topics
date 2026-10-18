using NorthWindSqlLiteApp1.Classes;
using NorthWindSqlLiteApp1.Classes.Core;
using Spectre.Console;

namespace NorthWindSqlLiteApp1;
internal partial class Program
{
    private static void Main(string[] args)
    {

        EmployeeOperations.ReportsToManager();
        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
        
    }
}
