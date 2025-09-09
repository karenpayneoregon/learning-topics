#nullable disable
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PartialSamples1.Classes.Extensions;

namespace PartialSamples1.Models;
public partial class Client
{
    public partial int Id { get; set => SetField(ref field, value); }
    public partial string FirstName 
    { 
        get; 
        set => SetField(ref field, value.CapitalizeFirstLetter());
    }

    public partial string LastName
    {
        get; 
        set => SetField(ref field, value.CapitalizeFirstLetter());
    }
    public partial Gender? Gender { get; set => SetField(ref field, value); }

    public override string ToString() => $"{Id,-4}{FirstName, -10} {LastName, -14} ({Gender})";

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new(propertyName));

    /// <summary>
    /// Sets the field to the specified value and raises the <see cref="PropertyChanged"/> event if the value has changed.
    /// </summary>
    /// <typeparam name="T">The type of the field.</typeparam>
    /// <param name="field">The field to set.</param>
    /// <param name="value">The value to set the field to.</param>
    /// <param name="propertyName">The name of the property. This is optional and will be automatically provided by the compiler.</param>
    /// <returns><c>true</c> if the field was changed; otherwise, <c>false</c>.</returns>
    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        
        field = value;
        
        OnPropertyChanged(propertyName);
        
        return true;
        
    }
}