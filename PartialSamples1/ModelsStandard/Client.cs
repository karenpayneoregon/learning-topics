#nullable disable

using System.ComponentModel;
using System.Runtime.CompilerServices;
using PartialSamples1.Classes.Extensions;

namespace PartialSamples1.ModelsStandard;
public class Client : INotifyPropertyChanged
{
    public int Id { get; set => SetField(ref field, value); }
    public string FirstName
    {
        get;
        set => SetField(ref field, value.CapitalizeFirstLetter());
    }

    public string LastName
    {
        get;
        set => SetField(ref field, value.CapitalizeFirstLetter());
    }
    public  Gender? Gender { get; set => SetField(ref field, value); }

    public override string ToString() => $"{Id,-4}{FirstName,-10} {LastName,-14} ({Gender})";

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new(propertyName));

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        
        field = value;
        
        OnPropertyChanged(propertyName);
        
        return true;
        
    }
}
