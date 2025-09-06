using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PartialSamples1.Models;
public partial class EmployeeWork
{
    public partial int Id { get; set => SetField(ref field, value); }

    public partial decimal HourlyWage
    {
        get; 
        set => SetField(ref field, value);
    }

    public partial int HoursWorked
    {
        get;
        set => SetField(ref field, value);
    }

    public partial decimal OvertimeRate
    {
        get;
        set => SetField(ref field, value);
    }

    public partial int OvertimeThreshold
    {
        get;
        set => SetField(ref field, value);
    }

    public partial decimal Salary
    {
        get;
        private set => SetField(ref field, value);
    }


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
