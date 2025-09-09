using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PartialSamples1.Models;

/// <summary>
/// Provides functionality for managing property setters with support for 
/// property change notifications. This class is designed to simplify 
/// property management by encapsulating the logic for setting fields 
/// and raising the <see cref="PropertyChanged"/> event when necessary.
/// </summary>
/// <remarks>
/// This class is intended to be used as a base class for models that 
/// require property change notifications, such as those implementing 
/// the <see cref="INotifyPropertyChanged"/> interface.
/// </remarks>
public class PropertySetters
{

    public event PropertyChangedEventHandler? PropertyChanged;

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
