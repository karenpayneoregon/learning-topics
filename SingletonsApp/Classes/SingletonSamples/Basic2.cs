using SingletonsApp.Models;

namespace SingletonsApp.Classes.SingletonSamples;

public sealed class Basic2
{
    private static readonly Lazy<Basic2> Lazy = new(() => new Basic2());
    public static Basic2 Instance => Lazy.Value;

    public TransactionInformation Transaction { get; set; }
    
    
    public void UpdateTransactionIdentifier()
    {
        TransactionInformationReader.Save(Transaction);
    }

    private Basic2()
    {
        var x = TransactionInformationReader.Load();
        
        Transaction = new TransactionInformation()
        {
            CurrentValue = x.CurrentValue
        };  
    }
}