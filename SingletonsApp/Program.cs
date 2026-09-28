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

        /// <summary>
        /// Retrieves and displays the database connection string and environment information.
        /// </summary>
        /// <remarks>
        /// This method utilizes the <see cref="EnvironmentSettings.Instance"/> singleton to access
        /// the database connection string and environment details. It also uses the 
        /// <see cref="SpectreConsoleHelpers.PrintPink"/> method to format the output.
        /// </remarks>
        private static void GetDatabaseConnectionString()
        {

            SpectreConsoleHelpers.PrintPink();
            
            var connectionString = EnvironmentSettings.Instance.DatabaseConnectionString;
            Console.WriteLine($"               Environment: {EnvironmentSettings.Instance.Environment}");
            Console.WriteLine($"Database Connection String: {connectionString}");
            Console.WriteLine();
        }

        /// <summary>
        /// Displays a list of months with their corresponding indices in the console.
        /// </summary>
        /// <remarks>
        /// This method retrieves the month list from the singleton instance of <see cref="Basic1"/> 
        /// and prints each month's index and name to the console. It also uses 
        /// <see cref="SpectreConsoleHelpers.PrintPink"/> to format the output.
        /// </remarks>
        private static void DisplayMonthList()
        {
            
            SpectreConsoleHelpers.PrintPink();
            
            var months = Basic1.Instance.MonthList;

            foreach (var (index, name) in months)
            {
                Console.WriteLine($"{index,-4} {name}");
            }

            Console.WriteLine();
            
        }

        /// <summary>
        /// Updates the transaction identifier by incrementing its current value and saving the updated value.
        /// </summary>
        /// <remarks>
        /// This method retrieves the current transaction identifier from the singleton instance of <see cref="Basic2"/>,
        /// increments its value using the <see cref="Helpers.NextValue"/> method, and updates the singleton instance.
        /// The updated value is then saved back to the configuration.
        /// </remarks>
        private static void UpdateTransactionIdentifier()
        {

            SpectreConsoleHelpers.PrintPink();

            // Get value from appsettings.json
            var transactionIdentifier = Basic2.Instance.Transaction.CurrentValue;
            Console.WriteLine($"Current Transaction Identifier: {transactionIdentifier}");
            // Increment the value by 1
            Helpers.NextValue(ref transactionIdentifier);
            Basic2.Instance.Transaction.CurrentValue = transactionIdentifier;
            // Save it back to the appsettings.json
            Basic2.Instance.UpdateTransactionIdentifier();

            Console.WriteLine($"Updated Transaction Identifier: {transactionIdentifier}");
            Console.WriteLine();
            
        }
    }
}
