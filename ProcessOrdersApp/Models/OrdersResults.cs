using CsvHelper.Configuration.Attributes;
using ProcessOrdersApp.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Serilog;

namespace ProcessOrdersApp.Models;
public class OrdersResults : INotifyPropertyChanged, IOrdersResults
{
    private static readonly ILogger Logger = Log.ForContext("Category", nameof(IOrdersResults));
    private bool _process;
    public bool Process { get => _process; set => SetField(ref _process, value); }
    [Index(0)]
    public int OrderID { get; set; }

    [Index(1)]
    public DateOnly OrderDate { get; set; }

    [Index(2)]
    public DateOnly RequiredDate { get; set; }

    [Index(3)]
    public DateOnly ShippedDate { get; set; }

    [Index(4)]
    public string ShipAddress { get; set; }

    [Index(5)]
    public string? ShipCity { get; set; }

    [Index(6)]
    public string? ShipPostalCode { get; set; }

    [Index(7)]
    public string? ShipCountry { get; set; }

    [Index(8)]
    public string CompanyName { get; set; } = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}

public class OrderResultItem
{
    public bool Process { get; set; } 

    public Order Data { get; set; } = null!;
}