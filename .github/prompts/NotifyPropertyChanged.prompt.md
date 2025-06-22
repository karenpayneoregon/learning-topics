Add INotifyPropertyChanged to the cuurent class
place field at top of class
remove unessary usings
use the following for property INotifyPropertyChanged set accessors
```csharp
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
```