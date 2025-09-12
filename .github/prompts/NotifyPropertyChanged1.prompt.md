
Add INotifyPropertyChanged to the cuurent class
Do not use backing fields
Getters example get;
Setters example set => SetField(ref field, value);
Use field keyword as shown below, make sure not to use field using the getter as shown below

```csharp
public string FirstName { get; set => SetField(ref field, value);}
```

remove unessary usings
do not add comments
Keep the properties at top of the class
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



