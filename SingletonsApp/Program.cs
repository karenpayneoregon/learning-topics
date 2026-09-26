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

            SpectreConsoleHelpers.PrintPink();

            var connectionString = EnvironmentSettings.Instance.DatabaseConnectionString;
            
            UpdateTransactionIdentifier();

            SpectreConsoleHelpers.ExitPrompt(Justify.Left);

        }

        private static void UpdateTransactionIdentifier()
        {
            var transactionIdentifier = Basic2.Instance.Transaction.CurrentValue;
            Helpers.NextValue(ref transactionIdentifier);
            Basic2.Instance.Transaction.CurrentValue = transactionIdentifier;
            Basic2.Instance.UpdateTransactionIdentifier();
        }
    }
}
