#nullable disable
using System.ComponentModel;

namespace PromptFilesExamplesApp1.Models;

internal partial class Person : Base, INotifyPropertyChanged
{
    public string FirstName
    {
        get => field.TrimEnd();
        set => SetField(ref field, value);
    }

    public string LastName
    {
        get => field.TrimEnd();
        set => SetField(ref field, value);
    }

    public DateTime BirthDate
    {
        get;
        set => SetField(ref field, value);
    }

    public Gender Gender
    {
        get;
        set => SetField(ref field, value);
    }

    public partial string Remarks { get; set; }

}