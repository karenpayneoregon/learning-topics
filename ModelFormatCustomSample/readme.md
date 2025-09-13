# Custom record format sample

```csharp
public string ToString(string? format, IFormatProvider? _)
    => format switch
    {
        "A" => $"{BirthDay.GetAge()}",
        "F" => $"{Id,-5}{FirstName} {LastName}",
        "N" => $"{FirstName} {LastName}",
        "B" => $"{BirthDay}",
        "I" => $"{Id}",
        _ => $"{Id,-3}{BirthDay} {LastName}, {BirthDay}"
    };
```