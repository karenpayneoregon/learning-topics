using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProcessOrdersApp.Models;
public class OrdersResults : INotifyPropertyChanged
{
    private bool _process;
    public bool Process { get => _process; set => SetField(ref _process, value); }
    public int OrderID { get; set; }

    public DateOnly OrderDate { get; set; }

    public DateOnly RequiredDate { get; set; }

    public DateOnly ShippedDate { get; set; }

    public string ShipAddress { get; set; }

    public string? ShipCity { get; set; }

    public string? ShipPostalCode { get; set; }

    public string? ShipCountry { get; set; }

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