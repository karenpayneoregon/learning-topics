using SingletonsApp.Classes.Core;
using SingletonsApp.Classes.SingletonSamples;
using SingletonsApp.Classes;
using SingletonsApp.Singletons;
using Spectre.Console;

namespace SingletonsApp
{
    internal partial class Program
    {
        static void Main(string[] args)
        {

            DisplayMonthList();

            GetDatabaseConnectionString();
            
            UpdateTransactionIdentifier();

            SpectreConsoleHelpers.ExitPrompt(Justify.Left);

        }

        private static void GetDatabaseConnectionString()
        {

            SpectreConsoleHelpers.PrintPink();
            
            var connectionString = EnvironmentSettings.Instance.DatabaseConnectionString;

            Console.WriteLine();
            
        }

        private static void DisplayMonthList()
        {
            
            SpectreConsoleHelpers.PrintPink();
            
            var months = Basic1.Instance.MonthList;

            foreach (var (index, name) in months)
            {
                Console.WriteLine($"{index}: {name}");
            }

            Console.WriteLine();
            
        }

        private static void UpdateTransactionIdentifier()
        {

            SpectreConsoleHelpers.PrintPink();

            // Get value from appsettings.json
            var transactionIdentifier = Basic2.Instance.Transaction.CurrentValue;
            // Increment the value by 1
            Helpers.NextValue(ref transactionIdentifier);
            Basic2.Instance.Transaction.CurrentValue = transactionIdentifier;
            // Save it back to the appsettings.json
            Basic2.Instance.UpdateTransactionIdentifier();

            Console.WriteLine();
            
        }
    }
}
