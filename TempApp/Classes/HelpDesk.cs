using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace TempApp.Classes;
public class HelpDesk : INotifyPropertyChanged
{
    public string Phone { get; set => SetField(ref field, value); }
    public string Email { get; set => SetField(ref field, value); }

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